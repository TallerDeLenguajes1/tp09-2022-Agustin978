// See https://aka.ms/new-console-template for more information
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

main();

void main()
{
    string carpeta = @"C:\Users\Agustin\Downloads";
    
    try
    {
        string[] archivos = Directory.GetFiles(carpeta);
        string nombreArchivo = @"C:\Cursos\Programacion_en_C_UNT\Taller_de_Lenguajes\tp09-2022-Agustin978\Lectura_Archivos_json\index";
        string formato = ".json";

        guardaArchivo(nombreArchivo, formato, archivos);
        muestraJson(nombreArchivo, formato);
    }
    catch (System.IO.DirectoryNotFoundException)
    {
        Console.WriteLine("Error en la   lectura del archivo :V");
    }
}

string CreaArchivoAGuardar(string nombreArchivo)
{
    
    return JsonSerializer.Serialize(nombreArchivo);
}

void guardaArchivo(string nombreArchivo, string formato, string[] archivos)
{
    //Console.WriteLine(archivos[0]);
    FileStream archivoJson = new FileStream(nombreArchivo+formato, FileMode.Create);
        
    using(StreamWriter strWriter = new StreamWriter(archivoJson))
    {
        foreach(var archivo in archivos)
        {
            string arcStr = CreaArchivoAGuardar(archivo); //Crea el string con el formato json a guardar
            strWriter.WriteLine("{0}",arcStr);
        }
        
    }
}

void muestraJson(string nombreArchivo, string formato)
{
    string line = "";
    FileStream archivoJson = new FileStream(nombreArchivo+formato, FileMode.Open);
    using(StreamReader strReader = new StreamReader(archivoJson))
    {
        while((line = strReader.ReadLine()) != null)
        {
            Console.WriteLine($"{line}\n");
        }
    }
}