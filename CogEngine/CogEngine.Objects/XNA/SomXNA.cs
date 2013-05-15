using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace CogEngine.Objects.XNA
{
    public class SomXNA : Som
    {
        public SomXNA(Jogo jogo) : base(jogo) { }

        private SoundEffect _Sound;

        public void Iniciar()
        {
            using (FileStream f = File.Open(CaminhoAbsoluto, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                _Sound = SoundEffect.FromStream(f);
            }
        }

        public void Tocar()
        {
            _Sound.Play();
        }
    }
}
