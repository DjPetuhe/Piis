
namespace SimplexLab.Structure
{
    internal class Simplex
    {
        private double[,] coefs;
        private double[] freeConst;
        private double[] minFuncCoefs;

        public Simplex(double[,] coefs, double[] freeConst, double[] minFuncCoefs)
        {
            this.coefs = coefs;
            this.freeConst = freeConst;
            this.minFuncCoefs = minFuncCoefs;
        }

        public void Solve()
        {
            Solve(Enumerable.Range(0, coefs.GetLength(0)).ToList());
        }
        public void Solve(List<int> startBasis)
        {
            Printer.PrintCurrentStep(coefs, freeConst, minFuncCoefs);
            if (!TransformToBasis(startBasis))
            {
                Printer.PrintUnsolvable();
                return;
            }
            while (!AllCoefsNegative())
            {
                Printer.PrintCurrentStep(coefs, freeConst, minFuncCoefs);
                List<int> possibleColumns = PossibleChoices();
                int pivot_j = -1;
                int pivot_i = -1;
                foreach (int col in possibleColumns)
                {
                    pivot_i = RatioTest(col);
                    if (pivot_i != -1)
                    {
                        pivot_j = col;
                        break;
                    }
                }
                if (pivot_j == -1)
                {
                    Printer.PrintUnsolvable();
                    return;
                }
                MoveBasis(pivot_i, pivot_j);
            }
            Printer.PrintResults(coefs, freeConst, minFuncCoefs);
        }

        private bool TransformToBasis(List<int> basis)
        {
            for (int i = 0; i < basis.Count; i++)
            {
                double temp_coef = Math.Pow(coefs[i, basis[i]], -1);
                for (int j = 0; j < coefs.GetLength(1); j++)
                {
                    coefs[i, j] *= temp_coef;
                }
                freeConst[i] *= temp_coef;
            }
            for (int k = 0; k < basis.Count; k++)
            {
                for (int i = k + 1; i < coefs.GetLength(0); i++)
                {
                    double temp_coef = coefs[i, basis[k]];
                    for (int j = 0; j < coefs.GetLength(1); j++)
                    {
                        coefs[i, j] -= coefs[k, j] * temp_coef;
                    }
                    freeConst[i] -= freeConst[k] * temp_coef;
                }
            }
            for (int k = basis.Count - 1; k >= 0; k--)
            {
                for (int i = 0; i < k; i++)
                {
                    double temp_coef = coefs[i, basis[k]];
                    for (int j = 0; j < coefs.GetLength(1); j++)
                    {
                        coefs[i, j] -= coefs[k, j] * temp_coef;
                    }
                    freeConst[i] -= freeConst[k] * temp_coef;
                }
            }
            for (int i = 0; i < basis.Count; i++)
            {
                double temp_coef = minFuncCoefs[basis[i]];
                for (int j = 0; j < minFuncCoefs.Length; j++)
                {
                    minFuncCoefs[j] -= coefs[i, j] * temp_coef;
                }
            }
            return true;
        }

        private bool AllCoefsNegative()
        {
            foreach(var coef in minFuncCoefs)
            {
                if (coef > 0) return false;
            }
            return true;
        }

        private List<int> PossibleChoices()
        {
            List<int> Possibleindexes = new();
            List<double> PossibleValues = new();
            for (int i = 0; i < minFuncCoefs.Length; i++)
            {
                if (minFuncCoefs[i] > 0)
                {
                    Possibleindexes.Add(i);
                    PossibleValues.Add(minFuncCoefs[i]);
                }
            }
            for (int i = 0; i < PossibleValues.Count; i++)
            {
                double maxVal = PossibleValues[i];
                int max_indx = i;
                int maxVal_indx = Possibleindexes[i];
                for (int j = i; j < PossibleValues.Count; j++)
                {
                    if (PossibleValues[j] > maxVal)
                    {
                        maxVal_indx = Possibleindexes[j];
                        maxVal = PossibleValues[j];
                        max_indx = j;
                    }
                }
                PossibleValues[max_indx] = PossibleValues[i];
                PossibleValues[i] = maxVal;
                Possibleindexes[max_indx] = Possibleindexes[i];
                Possibleindexes[i] = maxVal_indx;
                
            }
            return Possibleindexes;
        }

        private int RatioTest(int index)
        {
            int pivot_j = -1;
            double ratio = int.MaxValue;
            for (int j = 0; j < freeConst.Length; j++)
            {
                if (freeConst[j] > 0 && coefs[j, index] > 0)
                {
                    double temp_ratio = freeConst[j] / coefs[j, index];
                    if (temp_ratio < ratio)
                    {
                        ratio = temp_ratio;
                        pivot_j = j;
                    }
                }
            }
            return pivot_j;
        }

        private void MoveBasis(int pivot_i, int pivot_j)
        {
            double temp_coef = Math.Pow(coefs[pivot_i, pivot_j], -1);
            for (int j = 0; j < coefs.GetLength(1); j++)
            {
                coefs[pivot_i, j] *= temp_coef;
            }
            freeConst[pivot_j] *= temp_coef;
            for (int i = 0; i < coefs.GetLength(0); i++)
            {
                temp_coef = coefs[i, pivot_j];
                if (i != pivot_i && temp_coef != 0)
                {
                    for (int j = 0; j < coefs.GetLength(1); j++)
                    {
                        coefs[i, j] -= coefs[pivot_i, j] * temp_coef;
                    }
                    freeConst[i] -= freeConst[pivot_i] * temp_coef;
                }
            }
            temp_coef = minFuncCoefs[pivot_j];
            for (int i = 0; i < minFuncCoefs.Length; i++)
            {
                minFuncCoefs[i] -= coefs[pivot_i, i] * temp_coef;
            }
        }
    }
}
