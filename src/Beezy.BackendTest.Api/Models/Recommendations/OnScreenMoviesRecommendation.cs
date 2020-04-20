﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beezy.BackendTest.Api.Models.Recommendations
{
    /// <summary>
    /// Container model for suggested movies for each theater
    /// </summary>
    public class OnScreenMoviesRecommendation
    {
        /// <summary>
        /// Screen identifier
        /// </summary>
        public int Screen { get; set; }
        /// <summary>
        /// List of movies recommended to show
        /// </summary>
        public List<MoviesRecommendationResponse> Movies { get; set; }
    }
}
