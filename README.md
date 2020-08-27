# MiniJava

El repositorio corresponte a la fase #1 del proyecto de la clase de compiladores. Dicha fase consta de la creación de un analizador léxico para el lenguaje Java.

## Elementos del Proyecto 📋

El proyecto consta de 3 clases fundamentales. La primera clase es "Form1.cs", la cual se encarga de extraer el texto del archivo seleccionado, crear el archivo de salido con los tokens y errores, además, presentar cada uno de ellos de forma visual en pantalla.

La segunda clase es "Token.cs", la cual funciona como estructura de dato que se utilizara para recopilar cada token y error presentado en el archivo analizado.

La tercera clase es "AnalizadorLexico.cs", la cual contiene los métodos y funciones encargados de analizar el texto ingresado. 
Esta clase está conformada por 4 métodos, los cuales se encargan de generar las reglas del lenguaje y con ellas analizar el texto, estos métodos son:

### contarLineas
_Esta función se encarga de evaluar un token de entrada y verificar si existe un salto de línea, para así sumar 1 al contador de líneas y actualizar la posición de lectura._
```
private int contarLineas(string token, int indice, ref int inicioDeLinea)
```

### agregarToken
_Este método se encarga de generar el patron del analizador por medio de *expresiones regulares*, cada expresión regular que ingresa la concatena a la anterior y al mismo tiempo, guarda una lista con las expresiones regulares que debe omitir durante el análisis._
```
public void agregarToken(string expresion_regular, string nombre_token, bool ignorar = false)
```

### cargarExpresionesRegulares
_Este método se encarga de cargar el patron a una estructura *Regex* y de almacenar en una lista el indice de las expresiones regulares que debe tomar en cuenta al analizar. Además, solicita las opciones adicionales para generar la estructura Regex_
```
public void cargarExpresionesRegulares(RegexOptions options)
```