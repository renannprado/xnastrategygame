using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DummyProject.Types.Races
{
    class Human : IRace
    {
        private RaceType _type = RaceType.Human;

        public RaceType Type
        {
            get
            {
                return _type;
            }
        }

        public Human()
        { 
        
        }
    }
}
