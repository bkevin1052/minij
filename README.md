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
_Este método se encarga de cargar el patron a una estructura *Regex* y de almacenar en una lista el indice de las expresiones regulares que debe tomar en cuenta al analizar. Además, solicita las opciones adicionales para generar la estructura Regex._
```
public void cargarExpresionesRegulares(RegexOptions options)
```

### obtenerTokens
_Esta función es la más importante ya que se encarga de generar el *Match* entre el texto y la estructura Regex. La función retorna un *Token de Salida* con cada análisis realizado, siendo un token "ERROR" si no hace match con la estructura Regex o un token valido si hace match con alguna regla de la estructura Regex._
```
public IEnumerable<Token> obtenerTokens(string texto)
```
_Esta función debe ser IEnumerable ya que se utiliza la expresión "yiel return"_
```
yield return new Token(name, match.Value, match.Index, linea, (match.Index - inicio) + 1);
```
_Esta expresión retorna un valor y luego de retornarlo, regresa al ciclo donde se encontraba. Esto nos permite poder retornar cada token encontrado y luego continuar analizando el texto desde donde se encontro el token anterior._


## Lógica del Proyecto ⌨️

Al inicio se debe cargar un archivo (La interfaz gráfica del programa permite acceder al explorador de archivos y seleccionar uno) para obtener el contenido que se desea analizar. Luego el programa carga las expresiones regulares a la estructura Regex para generar las reglas de análisis.

Al momento de tener lista la estructura Regex se hace el primer match con el texto y se obtiene el indice del token de entrada con quien hizo match. Si el indice obtenido es mayor al indice actual del analizador, significa que existe data previa que no hizo match con la estructura Regex, esa data se cataloga como ERROR, se genera un token de salida (Detallando la data, la linea, la columna y el indice de ERROR) y se actualiza el indice actual del programa.

La data que hizo match con la estructura regex entra en un ciclo donde se determina la expresión regular con la que hizo match y se obtiene el nombre de la misma, para así generar un token de salida (Detallando la data, nombre de la expresión regular con la que hizo match, linea, columna y indice de la expresión VALIDA).

Luego de generar todos los tokens de salida se genera el archivo ".out", el cual contiene cada token valido y de error que se detectaron en el archivo. De igual forma los tokens de salida se presentan en pantalla por medio de la interfaz gráfica.

