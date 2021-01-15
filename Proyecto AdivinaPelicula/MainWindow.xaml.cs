using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro.Controls;

namespace Proyecto_AdivinaPelicula
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        //-----------------------------------VARIABLES-----------------------------------

        //Muscia
        bool reproduciendo = false;
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private readonly Random _random = new Random();

        //Puntuación
        int cont = 0;
        int puntuacion = 0;

        //Listas de películas
        ObservableCollection<Pelicula> listaJuego = new ObservableCollection<Pelicula>();
        ObservableCollection<Pelicula> listaPeliculas = Pelicula.GetSamples();

        //-----------------------------------MÉTODOS-----------------------------------
        public MainWindow()
        {
            InitializeComponent();
            ItemsListBox.DataContext = listaPeliculas;
            comenzarJuego();
            mostrarPelicula(0);

        }

        public void randPelicula()
        {

            int numRandom;
            int contPelis = 5;
            for (int i = 0; i < contPelis; i++)
            {
                numRandom = randomNumber(0, listaPeliculas.Count);

                //Evitar películas repetidas en el grupo de 5
                if (!listaJuego.Contains(listaPeliculas[numRandom])) listaJuego.Add(listaPeliculas[numRandom]);
                else contPelis++;

            }

            //Reiniciar la lista de pelis
            for (int i = 0; i < listaJuego.Count; i++)
            {
                if (!listaJuego[i].Acertada) listaJuego[i].Acertada = true;
                if (listaJuego[i].PistaUsada) listaJuego[i].PistaUsada = false;

            }

        }

        //Obtener número aleatorio
        public int randomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        //Comenzar el juego, init lista de 5, puntuacion reset a 0 y rand para 5 pelis aleatorias.
        private void comenzarJuego()
        {

            listaJuego = new ObservableCollection<Pelicula>();
            puntuacionTextBlock.Text = "0";
            randPelicula();

        }

        private void siguienteImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Control antidesbordes de la lista si es mayor el contador
            cont++;

            if (cont > listaJuego.Count - 1)
            {
                cont = listaJuego.Count - 1;
            }
            mostrarPelicula(cont);

        }

        private void anteriorImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Control antidesbordes de la lista si es menor el contador
            cont--;

            if (cont < 0)
            {
                cont = 0;
            }
            mostrarPelicula(cont);
        }

        private void mostrarPelicula(int pelicula)
        {
            //Gestionar la parada de musica si se muestra una nueva peli
            mediaPlayer.Stop();
            reproduciendo = false;

            //Vaciar el texto a validar y cambiar el contador de pelis
            textoValidar.Text = "";
            paginacionPeliculaTextBlock.Text = pelicula + 1 + "/" + listaJuego.Count;

            //Establecer el DataContext de las 5 pelis
            ContenedorAdivinar.DataContext = listaJuego[pelicula];

        }

        private void validarPelicula_Click(object sender, RoutedEventArgs e)
        {
            int puntosGanados = 0;

            //Control de minusculas y mayusculas en la validación
            if (textoValidar.Text.ToUpper() == listaJuego[cont].Titulo.ToUpper())
            {

                listaJuego[cont].Acertada = false;

                //Comprobaciones de puntuación según dificultad
                if (listaJuego[cont].Dificultad_enum == Pelicula.Dificultad.Fácil)
                {

                    if (verPistaCheckBox.IsChecked == true)
                    {
                        puntuacion += 30 / 2;
                        puntosGanados = 30 / 2;
                    }
                    else
                    {
                        puntuacion += 30;
                        puntosGanados = 30;
                    }

                    puntuacionTextBlock.Text = puntuacion.ToString();
                }
                else if (listaJuego[cont].Dificultad_enum == Pelicula.Dificultad.Normal)
                {
                    if (verPistaCheckBox.IsChecked == true)
                    {
                        puntuacion += 50 / 2;
                        puntosGanados = 50 / 2;
                    }
                    else
                    {
                        puntuacion += 50;
                        puntosGanados = 50;
                    }

                    puntuacionTextBlock.Text = puntuacion.ToString();
                }
                else if (listaJuego[cont].Dificultad_enum == Pelicula.Dificultad.Difícil)
                {
                    if (verPistaCheckBox.IsChecked == true)
                    {
                        puntuacion += 100 / 2;
                        puntosGanados = 100 / 2;
                    }
                    else
                    {
                        puntuacion += 100;
                        puntosGanados = 100;
                    }

                }

                if (listaJuego[cont].PistaAudioUsada)
                {
                    puntuacion -= 5;
                    puntosGanados -= 5;

                }

                puntuacionTextBlock.Text = puntuacion.ToString();


                //Si se acierta la peli, la musica para
                mediaPlayer.Stop();
                reproduciendo = false;

                MessageBox.Show("¡Has acertado y has obtenido " + puntosGanados + " puntos!", "Validar película", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                MessageBox.Show("Has fallado...", "Validar película", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void nuevaPartidaButton_Click(object sender, RoutedEventArgs e)
        {
            //Comprobación de que tengamos en la lista al menos 5 peliculas
            if (listaPeliculas.Count < 5)
            {
                MessageBox.Show("No se ha podido iniciar nueva partida, hay menos de 5 películas. Por favor añada más películas en la sección de gestionar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //Comenzando nueva partida
                MessageBoxResult result = MessageBox.Show("Se cargarán 5 nuevas películas y tu puntuación volverá a 0", "¿Iniciar nueva partida?", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:

                        comenzarJuego();
                        cont = 0;
                        mostrarPelicula(cont);                      
                        break;

                }

                //Reset de puntuación
                puntuacion = 0;
            }


        }

        private void añadirPeliculaButton_Click(object sender, RoutedEventArgs e)
        {

            bool peliculaYaExiste = false;
            //Comprobación de campos antes de añadir nueva película
            if (añadirTituloTextBox.Text == "" || añadirPistaTextBox.Text == "" || añadirImagenTextBox.Text == "" || añadirSonidoTextBox.Text == "" || añadirDificultadComboBox.SelectedItem == null || añadirGeneroComboBox.SelectedItem == null)
            {
                MessageBox.Show("No puede haber campos vacios, por favor rellene todos los campos", "Añadir película", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //Instancia de nuevo objeto Pelicula
                Pelicula peliculaAñadir = new Pelicula(añadirTituloTextBox.Text, añadirPistaTextBox.Text, añadirGeneroComboBox.SelectedValue.ToString(), añadirDificultadComboBox.SelectedValue.ToString(), añadirImagenTextBox.Text, true, false, añadirSonidoTextBox.Text, false);

                //Comprobación de si ya existe un titulo igual (.contains no funcionaba)
                for (int i = 0; i < listaPeliculas.Count; i++)
                {
                    if (listaPeliculas[i].Titulo == peliculaAñadir.Titulo)
                    {
                        peliculaYaExiste = true;
                    }


                }

                if (peliculaYaExiste)
                {
                    MessageBox.Show("La película " + peliculaAñadir.Titulo + " ya existe en la lista. Por favor añada otra diferente.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    listaPeliculas.Add(peliculaAñadir);
                    MessageBox.Show("Pélicula " + peliculaAñadir.Titulo + "añadida con exito", "Añadir película", MessageBoxButton.OK, MessageBoxImage.Information);

                }

            }
        }

        //Dialogo para del boton de añadir imagen
        private void examinarImagenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;)| All files (*.*)|*.*";
            openFileDialog.ShowDialog();
            añadirImagenTextBox.Text = openFileDialog.FileName;
        }

        //Eliminar una pelicula
        private void eliminarPeliculaButton_Click(object sender, RoutedEventArgs e)
        {
            listaPeliculas.Remove((Pelicula)ItemsListBox.SelectedItem);

        }

        //Carga un JSON con un dialogo
        private void cargarJSONButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Files | *.json;)| All files (*.*)|*.*";
            openFileDialog.ShowDialog();

            try
            {
                using (StreamReader jsonStream = File.OpenText(openFileDialog.FileName))
                {
                    var json = jsonStream.ReadToEnd();
                    ObservableCollection<Pelicula> pelisJSON = JsonConvert.DeserializeObject<ObservableCollection<Pelicula>>(json);
                    listaPeliculas.Clear();

                    foreach (Pelicula p in pelisJSON)
                    {
                        listaPeliculas.Add(p);
                    }

                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("No se puede dejar vacio el nombre de la ruta de acceso. El JSON no se cargará", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Guardar la lista de pelis en un JSON con dialogo
        private void guardarJSONButton_Click(object sender, RoutedEventArgs e)
        {

            string peliculasJson = JsonConvert.SerializeObject(listaPeliculas, Formatting.Indented);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON Files | *.json;)| All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {

                File.WriteAllText(saveFileDialog.FileName, peliculasJson);
            }

        }

        //Icono para escuchar la banda sonora
        private void pistaAudioImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Open(new Uri(listaJuego[cont].PistaAudio, UriKind.RelativeOrAbsolute));
            mediaPlayer.Volume = 0.5;

            //Funcionalidad de reproducir/parar cuando se clica de nuevo
            if (reproduciendo)
            {
                mediaPlayer.Stop();
                reproduciendo = false;
            }
            else
            {
                listaJuego[cont].PistaAudioUsada = true;
                mediaPlayer.Play();
                reproduciendo = true;

            }

        }

        //Examinar una banda sonora para añadir a una película
        private void examinarMusicaButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "mp3 files (*.mp3)|*.mp3|m4a files(*.m4a)|*.m4a|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true) añadirSonidoTextBox.Text = openFileDialog.FileName;

        }

        //Reglas del juego
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("¡Bienvenido al juego de adivinar películas!\r" +
                "Adivinar una película te da puntos dependiendo de su dificultad.\r" +
                "Fácil --> 30 puntos\r" +
                "Normal --> 50 puntos\r" +
                "Difícil --> 100 puntos\r" +
                "¡Ver la pista reducirá tus puntos a la mitad!\r" +
                "¡Usar la pista de la banda sonora te restará 5 puntos!\r" +
                "Dicho esto ¡Buena suerte!", "Reglas de juego", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
