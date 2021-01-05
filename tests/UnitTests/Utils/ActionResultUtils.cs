using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyWebAPITemplate.UnitTests.Utils
{
    public static class ActionResultUtils
    {
        /// <summary>
        /// Extension method for getting the ActionResult's content value
        /// This is helper method, because getting the content from ActionResult
        /// is kinda tedious job to do. 
        /// Read more: https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static T GetObjectResult<T>(this ActionResult<T> result)
        {
            if (result.Result != null)
                return (T)((ObjectResult)result.Result).Value;
            return result.Value;
        }
    }
}
