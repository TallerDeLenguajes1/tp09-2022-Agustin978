// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

Random rand = new Random();
main();

void main()
{
    //Random rand = new Random();
    string path = @"C:\Cursos\Programacion_en_C_UNT\Taller_de_Lenguajes\tp09-2022-Agustin978\ListaProductos\ListaProductos\index";
    string formato = ".json";
    List<Producto> Productos = new List<Producto>();
    int cantProductos = rand.Next(1,3);

    for(int i = 0; i < cantProductos; i++)
    {   
        Console.WriteLine($"\nIngreso del producto {i}");
        Productos.Add(creaProducto());
    }
    
    //GuardaArchivoJSON(path, formato, Productos);
    SerializeJsonFile(Productos, path, formato);
    muestraJson(path, formato);

    //Deserealizado del archivo Json
    string prodDeJson = GetObjectJsonFromFile(path, formato);
    DeserializeJson(prodDeJson);
}

Producto creaProducto()
{
    string nombre = "";
    do
    {
        Console.WriteLine("\nIngrese el nombre del producto a ingresar");
        nombre = Console.ReadLine();
    }while(string.IsNullOrEmpty(nombre));

    DateTime fechaVence = randomDate();

    float precio = valorAleatorio();

    int tamanio = rand.Next();

    Producto prod = new Producto(nombre, fechaVence, precio, tamanio.ToString());
    
    return prod;
}

//Para retornar una fecha de vencimiento aleatoria entre 01/01/2019 y el dia de hoy
DateTime randomDate()
{
    //Random rand = new Random();
    DateTime start = new DateTime(2019, 1, 1);
    int range = (DateTime.Now - start).Days;
    DateTime fechaVence = start.AddDays(rand.Next(range));
    return fechaVence;
}

//Funcion para obtener un valor flotante aleatorio entre 1000 y 5000
float valorAleatorio()
{
    //Random rand = new Random();
    double min = 1000;
    double max = 5000;
    double rango = max - min;

    double inicial = rand.NextDouble();
    float numero = (float)((inicial * rango) + min);
    return numero;
}

/*
void GuardaArchivoJSON(string path, string formato, List<Producto> Productos)
{
    FileStream archivoJson = new FileStream(path+formato, FileMode.OpenOrCreate);

    using(StreamWriter strWrter = new StreamWriter(archivoJson))
    {
        for (int i = 0; i < Productos.Count; i++)
        {
            Producto prodIngresa = Productos[i];
            string arcStr = creaArchivoAGuardar(prodIngresa);
            strWrter.Write("{0}\n", arcStr);
        }
    }
}

string creaArchivoAGuardar(Producto prod)
{
    return JsonSerializer.Serialize(prod);
    //Console.WriteLine(archivoAGuardar);
}
*/

//Serializado y guardado del archivos json
void SerializeJsonFile(List<Producto> Productos, string path, string formato)
{
    string prodJson = JsonConvert.SerializeObject(Productos.ToArray(), Formatting.Indented);
    FileStream archivoJson;


    if (!File.Exists(path+formato))
    {
        archivoJson = new FileStream(path + formato, FileMode.OpenOrCreate);
    }else
    {
        archivoJson = new FileStream(path + formato, FileMode.Append);
    }
    

    using(StreamWriter strWriter = new StreamWriter(archivoJson))
    {
        strWriter.WriteLine(prodJson);
    }
}



//Funcion para obtener los elementos del json
string GetObjectJsonFromFile(string nombreArchivo, string formato)
{
    string ObjectJson;
    FileStream archivoJson = new FileStream(nombreArchivo + formato, FileMode.Open);

    using(StreamReader strReader = new StreamReader(archivoJson))
    {
        ObjectJson = strReader.ReadToEnd(); //Lee todos los elementos ingresados en el archivo json
    }

    return ObjectJson; //Retorna lo que se encontro en el archivo json
}

//Deserialize del archivo json
void DeserializeJson(string elementosJson)
{
    string[] elementosSeparado = elementosJson.Split(']');
    foreach(var elemento in elementosSeparado)
    {
        //Console.WriteLine(elemento+"]");
        List<Producto> Productos = JsonConvert.DeserializeObject<List<Producto>>(elemento+"]"); //Deserealiza lo encontrado en el string de elementos que lee los datos del json
        mostrar(Productos);
    }
    //List<Producto> Productos = JsonConvert.DeserializeObject<List<Producto>>(elementosJson); //Deserealiza lo encontrado en el string de elementos que lee los datos del json
    //mostrar(Productos);
}


//Mostrado del archivo Json sin deserealizarlo
void muestraJson(string nombreArchivo, string formato)
{
    string line = "";
    FileStream archivoJson = new FileStream(nombreArchivo + formato, FileMode.Open);
    using (StreamReader strReader = new StreamReader(archivoJson))
    {
        while ((line = strReader.ReadLine()) != null)
        {
            Console.WriteLine($"{line}\n");
        }
    }
}


//Muestra los elementos de la lista
void mostrar(List<Producto> Productos)
{
    Console.WriteLine("\n\nMuestra Productos");
    foreach (var producto in Productos)
    {
        Console.WriteLine($"{producto.nombre}\n");
        Console.Write($"{producto.fechaVencimiento}\n");
        Console.WriteLine($"{producto.precio}U$D\n");
    }
}
