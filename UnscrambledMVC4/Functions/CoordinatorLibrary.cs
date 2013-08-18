using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;
using Unscrambled.Models;

namespace Unscrambled.Functions
{
    public class CoordinatorLibrary
    {

        public String GenerateJoinCode()
        {
            UnscrambledDBEntities db = new UnscrambledDBEntities();

            String JoinCode;
            bool JoinCodeAlreadyExists;
            JoinCode = generateRandomString(6);

            for (int i = 1; i <= 5; i++)
            {
                JoinCodeAlreadyExists = db.Games.Where(g => g.JoinCode == JoinCode).Count() > 0;
                if (!JoinCodeAlreadyExists)
                {
                    break;
                }
                else
                {
                    JoinCode = generateRandomString(6);
                }
            }
            return JoinCode;
        }



        private String generateRandomString(int length)
        {
            Random random = new Random();
            String randomString = "";
            int randNumber;

            //Loop ‘length’ times to generate a random number or character
            for (int i = 0; i < length; i++)
            {
                if (random.Next(1, 3) == 1)
                    randNumber = random.Next(97, 123); //char {a-z}
                else
                    randNumber = random.Next(48, 58); //int {0-9}

                //append random char or digit to random string
                randomString = randomString + (char)randNumber;
            }
            //return the random string
            return randomString;
        }
    }



        



  
}