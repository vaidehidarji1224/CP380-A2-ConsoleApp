using System;

namespace RatingAdjustment.Services
{
    /** Service calculating a star rating accounting for the number of reviews
     * 
     */
    public class RatingAdjustmentService
    {
        const double MAX_STARS = 5.0;  // Likert scale
        const double Z = 1.96; // 95% confidence interval

        double _q;
        double _percent_positive;

        /** Percentage of positive reviews
         * 
         * In this case, that means X of 5 ==> percent positive
         * 
         * Returns: [0, 1]
         */
        void SetPercentPositive(double stars)
        {
             _percent_positive = stars / 5;  // created method 
        }

        /**
         * Calculate "Q" given the formula in the problem statement
         */
        void SetQ(double number_of_ratings)
        {
            
         _q = Z * Math.Sqrt((_percent_positive * (1 - _percent_positive) + Z * Z / (4 * number_of_ratings)) / number_of_ratings);

        }
        public double Adjust(double stars, double number_of_ratings) {
            // TODO: Implement this!
            SetPercentPositive(stars);
            SetQ(number_of_ratings);

            double lvalue = (_percent_positive + ((Z * Z) / (2 * number_of_ratings)) - _q) / (1 + ((Z * Z) / number_of_ratings)) * 5;
            if (lvalue <= 5.0)
            { return lvalue; }
            else
                return stars;

        }
    }
}
 