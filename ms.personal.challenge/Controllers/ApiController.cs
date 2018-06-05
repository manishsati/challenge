using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MS.Personal.Challenge.Controllers
{
    [Route("[controller]/[action]")]
    public class ApiController : Controller
    {

        const string invalidRequest= "The request is invalid.";

        /// <summary>
        /// returns the nth number in fibonacci sequence
        /// </summary>
        /// <param name="n">The index (n) of the fibonacci sequence</param>
        /// <returns></returns>
        public IActionResult Fibonacci(long? n)
        {
            Trace.WriteLine("fibnacci" + n);

            if (!n.HasValue)
            {
                return BadRequest(invalidRequest);
            }
            else if (n >= 0 && n <= 92)
            {
                return Ok(GetFibonacci((int)n));
            }
            else if (n >= -92 && n < 0)
            {
                return Ok(Math.Pow(-1, n.Value + 1) * GetFibonacci((int)n * -1));
            }
            else
            {
                return BadRequest("No Content");
            }
        }

        private long GetFibonacci(int index)
        {           
            var terms = new List<long> { 0, 1 };
            int i = 2;

            while (i <= index + 1)
            {
                terms.Add(terms[i - 1] + terms[i - 2]);
                i += 1;
            }

            return Math.Abs(terms[index]);
        }
        /// <summary>
        /// return static token in guid format
        /// </summary>
        /// <returns></returns>
        public Guid Token()
        {
            Trace.WriteLine("Token request");

            return new Guid("98459414-a266-4567-aab0-1cc1cbdb620f");
        }

        /// <summary>
        /// reverse words in given sentence
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public IActionResult ReverseWords(string sentence)
        {
            Trace.WriteLine("Sentence: " + sentence);

            if (!string.IsNullOrEmpty(sentence))
            {
                sentence = string.Join(" ", from word in sentence.Split(" ", StringSplitOptions.None)
                                            select new string(word.Reverse().ToArray()));
            }
            return Ok(sentence);
        }

        /// <summary>
        /// Returns the type of triange given the lengths of its sides
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>Triangle type</returns>
        public IActionResult TriangleType(int? a, int? b, int? c)
        {
            Trace.WriteLine("TriangleType type parameters a: {a} b: {b} c: {c}");

            if (!a.HasValue || !b.HasValue || !c.HasValue)
            {
                return BadRequest(invalidRequest);
            }
            else
            {
                var sides = new List<ulong>() { (ulong)a.Value, (ulong)b.Value, (ulong)c.Value };

                var sideCount = sides.Distinct().Count();

                ulong largest = sides.Max();

                ulong perimeter = sides[0] + sides[1] + sides[2];

                bool isvalid = a >= 0 && b >= 0 && c >= 0 && perimeter > 0 && largest < (perimeter - largest);

                var triangleType = isvalid ? sideCount == 1 ? "Equilateral" : sideCount == 2 ? "Isosceles" : sideCount == 3 ? "Scalene" : "Error" : "Error";

                return Ok(triangleType);
            }
        }
    }
}
