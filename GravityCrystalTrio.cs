using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyHollow
{
    public class GravityCrystalTrio
    {
        private UnityEngine.GameObject _one;
        private UnityEngine.GameObject _two;
        private UnityEngine.GameObject _three;
        public UnityEngine.GameObject One => _one;
        public UnityEngine.GameObject Two => _two;
        public UnityEngine.GameObject Three => _three;

        public GravityCrystalTrio(UnityEngine.GameObject one, UnityEngine.GameObject two, UnityEngine.GameObject three)
        {
            _one = one;
            _two = two;
            _three = three;
        }
    }
}
