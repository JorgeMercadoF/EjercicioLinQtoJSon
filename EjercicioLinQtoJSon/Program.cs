using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioLinQtoJSon
{

    #region Clases API
    class Posts
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

    class Comments
    {
        public int PostId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
    }

    class Albums
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
    }

    class Photos
    {
        public int AlbumId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
    }

    class Todos
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Completed { get; set; }
    }

    class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Adress Adress { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public Company Company { get; set; }
    }

    class Geo
    {
        public float Lat { get; set; }
        public float Lng { get; set; }
    }

    class Adress
    {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public Geo Geo { get; set; }
    }

    class Company
    {
        public string Name { get; set; }
        public string CatchPhrase { get; set; }
        public string Bs { get; set; }
    }
    #endregion

    #region Metodos y Main
    class Program
    {
        //Ver cuales son los comentarios que hay para un post determinado.
        static async void Ejercicio1(int postId)
        {
            using (var Clientes = new HttpClient())
            {
                string urlComments = @"http://jsonplaceholder.typicode.com/comments";
                var uriComments = new Uri(urlComments);

                var responseComments = await Clientes.GetAsync(uriComments);
                if (responseComments.IsSuccessStatusCode)
                {
                    var contentComments = await responseComments.Content.ReadAsStringAsync();
                    JArray ContenidoComments = JArray.Parse(contentComments);
                    var comentariosPostId = from x in ContenidoComments
                                            where (int)x["postId"] == postId
                                            select x;

                    foreach (JToken comentario in comentariosPostId)
                    {
                        Console.WriteLine("Titulo: " + comentario["name"].ToString() + " [" + comentario["email"].ToString() + "] " + "\n" + comentario["body"] + "\n\n");
                    }
                }
            }
        }

        //ver cuales son los albums que hay y la cantidad de fotos que contiene cada uno de ellos
        static async void Ejercicio2()
        {
            using (var Clientes = new HttpClient())
            {
                string urlAlbums = @"http://jsonplaceholder.typicode.com/albums";
                string urlPhotos = @"http://jsonplaceholder.typicode.com/photos";
                var uriAlbums = new Uri(urlAlbums);
                var uriPhotos = new Uri(urlPhotos);

                var responseAlbums = await Clientes.GetAsync(uriAlbums);
                var responsePhotos = await Clientes.GetAsync(uriPhotos);
                if (responseAlbums.IsSuccessStatusCode && responsePhotos.IsSuccessStatusCode)
                {
                    var contentAlbums = await responseAlbums.Content.ReadAsStringAsync();
                    var contentPhotos = await responsePhotos.Content.ReadAsStringAsync();
                    JArray ContenidoAlbums = JArray.Parse(contentAlbums);
                    JArray ContenidoPhotos = JArray.Parse(contentPhotos);
                    var albumes = from x in ContenidoAlbums
                                  select x;

                    foreach (JToken album in albumes)
                    {
                        var photos = from y in ContenidoPhotos
                                     where (int)y["albumId"] == ((int)album["id"])
                                     select y;
                        Console.WriteLine("El album: " + album["title"].ToString() + " contiene: " + photos.Count() + "\n\n");
                    }
                }
            }
        }

        //ver cuales son los nombres de los usuarios ordenados descendentemente.
        static async void Ejercicio3()
        {
            using (var Clientes = new HttpClient())
            {
                string urlUsers = @"http://jsonplaceholder.typicode.com/users";
                var uriUsers = new Uri(urlUsers);

                var responseUsers = await Clientes.GetAsync(uriUsers);
                if (responseUsers.IsSuccessStatusCode)
                {
                    var contentUsers = await responseUsers.Content.ReadAsStringAsync();
                    JArray ContenidoUsers = JArray.Parse(contentUsers);
                    var users = from x in ContenidoUsers
                                orderby x["name"] descending
                                select x;

                    foreach(var user in users)
                    {
                        Console.WriteLine(user["name"].ToString() + "\n");
                    }
                }
            }
        }

        //Ver que cantidad de comentarios totales ha recibido un usuario.
        static async void Ejercicio4(int idUsers)
        {

        }

        static void Main(string[] args)
        {
            //Ejercicio1(2);
            //Ejercicio2();
            Ejercicio3();
            Console.ReadLine();
        }
    }
    #endregion
}
