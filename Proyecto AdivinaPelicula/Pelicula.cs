using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;


namespace Proyecto_AdivinaPelicula
{
    class Pelicula : INotifyPropertyChanged
    {
        private string _titulo;

        public string Titulo
        {
            get { return _titulo; }
            set
            {
                _titulo = value;
                NotifyPropertyChanged("Titulo");
            }
        }

        //------------------------------------------//

        private string _pista;

        public string Pista
        {
            get { return _pista; }
            set
            {
                _pista = value;
                NotifyPropertyChanged("Pista");
            }
        }

        //------------------------------------------//


        public enum Genero
        {
            Acción,
            Ciencia_ficción,
            Comedia,
            Drama,
            Terror
        }
        private Genero _genero;

        public Genero Genero_enum
        {
            get { return _genero; }
            set
            {
                _genero = value;
                NotifyPropertyChanged("Genero_enum");
            }
        }

        //------------------------------------------//
        public enum Dificultad
        {
            Fácil, Normal, Difícil
        }

        private Dificultad _dificultad;

        public Dificultad Dificultad_enum
        {
            get { return _dificultad; }
            set
            {
                _dificultad = value;
                NotifyPropertyChanged("Dificultad_enum");
            }
        }

        //------------------------------------------//

        private string _imagen;

        public string Imagen
        {
            get { return _imagen; }
            set
            {
                _imagen = value;
                NotifyPropertyChanged("Imagen");
            }
        }

        private bool _acertada;

        //------------------------------------------//

        public bool Acertada
        {
            get { return _acertada; }
            set
            {
                _acertada = value;
                NotifyPropertyChanged("Acertada");
            }
        }

        //------------------------------------------//

        private bool _pistaUsada;

        public bool PistaUsada
        {
            get { return _pistaUsada; }
            set
            {
                _pistaUsada = value;
                NotifyPropertyChanged("PistaUsada");
            }
        }

        //------------------------------------------//

        private string _pistaAudio;

        public string PistaAudio
        {
            get { return _pistaAudio; }
            set
            {
                _pistaAudio = value;
                NotifyPropertyChanged("PistaAudio");
            }
        }

        //------------------------------------------//

        private bool _pistaAudioUsada;

        public bool PistaAudioUsada
        {
            get { return _pistaAudioUsada; }
            set
            {
                _pistaAudioUsada = value;
                NotifyPropertyChanged("PistaAudioUsada");
            }
        }

        //------------------------------------------//

        public Pelicula(string titulo, string pista, string genero_enum, string dificultad_enum, string imagen, bool acertada, bool pistaUsada, string pistaAudio, bool pistaAudioUsada)
        {
            Titulo = titulo;
            Pista = pista;         
            Genero_enum = (Genero)Enum.Parse(typeof(Genero), genero_enum);
            Dificultad_enum = (Dificultad)Enum.Parse(typeof(Dificultad), dificultad_enum);
            Imagen = Path.Combine("/Peliculas", imagen);
            Acertada = acertada;
            PistaUsada = pistaUsada;
            PistaAudio = pistaAudio;
            PistaAudioUsada = pistaAudioUsada;


        }

        public static ObservableCollection<Pelicula> GetSamples()
        {
            ObservableCollection<Pelicula> lista = new ObservableCollection<Pelicula>();
   
            lista.Add(new Pelicula("El caballero oscuro", "Un murcielago y un payaso", "Acción", "Normal", "Accion/El caballero oscuro.jpg", true, false, "El caballero oscuro.m4a", false));
            lista.Add(new Pelicula("En busca del arca perdida", "Nunca deja atrás su sombrero", "Acción", "Difícil", "Accion/En busca del arca perdida.jpg", true, false, "En busca del arca perdida.m4a", false));
            lista.Add(new Pelicula("Los vengadores", "Un grupo variopinto con ganas de venganza", "Acción", "Fácil", "Accion/Los vengadores.png", true, false, "Los vengadores.m4a", false));
            lista.Add(new Pelicula("Logan", "El lobo y la niña", "Ciencia_ficción", "Normal", "Ciencia ficción/Logan.png", true, false, "Logan.m4a", false));
            lista.Add(new Pelicula("Parque jurasico", "¿Que tiene dientes y millones de años?", "Ciencia_ficción", "Normal", "Ciencia ficción/Parque jurasico.png", true, false, "Parque jurasico.m4a", false));
            lista.Add(new Pelicula("Star wars", "Una princesa, un piloto, un contrabandista y un felpudo", "Ciencia_ficción", "Fácil", "Ciencia ficción/Star wars.png", true, false, "Star wars.m4a", false));
            lista.Add(new Pelicula("El Grinch", "Es verde y odia la navidad", "Comedia", "Normal", "Comedia/El Grinch.png", true, false, "El Grinch.m4a", false));
            lista.Add(new Pelicula("Kingsman", "Una agencia secreta desternillante", "Comedia", "Difícil", "Comedia/Kingsman, servicio secreto.png", true, false, "Kingsman.m4a", false));
            lista.Add(new Pelicula("Spiderman en el spiderverso", "Ocho patas en multiples universos", "Comedia", "Difícil", "Comedia/Spiderman en el spiderverso.png", true, false, "Spiderman en el spiderverso.m4a", false));
            lista.Add(new Pelicula("Descifrando enigma", "Seguro que tu puedes descifrar el enigma", "Drama", "Difícil", "Drama/Descifrando enigma.png", true, false, "Descifrando enigma.m4a", false));
            lista.Add(new Pelicula("El club de la lucha", "La primera reglade nuestro club es que no se habla del club", "Drama", "Normal", "Drama/El club de la lucha.jpg", true, false, "El club de la lucha.m4a", false));
            lista.Add(new Pelicula("El renacido", "Volviendo a la vida", "Drama", "Difícil", "Drama/El renacido.png", true, false, "El renacido.m4a", false));
            lista.Add(new Pelicula("Alien", "En el espacio nadie puede oir tus gritos", "Terror", "Normal", "Terror/Alien, el octavo pasajero.jpg", true, false, "Alien.m4a", false));
            lista.Add(new Pelicula("IT", "Globo rojo, chubasquero amarillo", "Terror", "Fácil", "Terror/It.png", true, false, "It.m4a", false));
            lista.Add(new Pelicula("Rec", "Una cámara terrorifica", "Terror", "Fácil", "Terror/Rec.png", true, false, "Rec.m4a", false));


            return lista;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
