using System;

public class Producto
{
	public string nombre { get; set; }
	public DateTime fechaVencimiento { get; set; }
	public float precio { get; set; }
	public string tamanio { get; set; }

    //Constructor de clase
	public Producto(string nombre, DateTime fechaVencimiento, float precio, string tamanio)
    {
        this.nombre = nombre;
        this.fechaVencimiento = fechaVencimiento;
        this.precio = precio;
        this.tamanio = tamanio;
    }
}