# Laboratorio - Analisis Sintactico Descendente Recursivo

El repositorio corresponte al primer laboratorio de la clase de compiladores. Dicha laboratorio consta de la creación de un analizador sintactico para la gramatica establecida. En nuestro caso se nos solicito realizar los metodos:
* For
* Return
* Expr


## Elementos del Laboratorio 📋

El laboratorio consta de 2 partes fundamentales. La primera parte es "AnalizadorLexico.cs", la cual se encarga de realizar un analisis sintactico del archivo de entrada, ademas, crea una lista de tokens que se utilizan posteriormente en la parte de analisis sintactico.

La segunda parte es el "AnalizadorSintactico.cs", el cual es la parte central de este laboratorio, se encarga de validar el orden de la secuencia de tokens por lo que utiliza una logica de analisis sintacticos descendente recursivo.

Para implementar un analisis descente recursivo se necesitan de dos conceptos basicos como lo son:

### matchToken
_Esta función se encarga de evaluar el token de entrada actual y verificar si coincide con el valor esperado, para luego retornar un valor de salida que corresponda a dicha validacion._
```
private bool matchToken(string type)
```

### lookAhead
_Esta variable nos permite visualizar el siguiente token sin necesidad de mover el index a su posicion, esto nos ayuda a determinar el camino descendente de nuestro analizador al momento de evaluar una sentencia de entrada._
```
private int lookAhead;
```

### matchToken con lookAhead
_Al incorporar la variable lookAhead a la funcion matchToken() se obtiene una funcion capaz de evaluar una posicion delante con el fin de anticipar la ruta de derivacion para una produccion._
```
    bool value = false;
        if (lookAhead == tokens.Count())
            return false;
        if (tokens[lookAhead].Nombre == type)
        {
            tokenActual = lookAhead;
            lookAhead++;
            value = true;
        }

    return value;
```


## Lógica del Proyecto ⌨️

Al inicio se debe cargar un archivo (La interfaz gráfica del programa permite acceder al explorador de archivos y seleccionar uno) para obtener el contenido que se desea analizar. Luego el programa carga las expresiones regulares a la estructura Regex para generar las reglas de análisis.

Al momento de tener lista la estructura Regex se procede a realizar el analisis lexico del archivo, al finalizar el analisis se genera una lista de tokens de salida. Esta lista sirve de base para la siguiente fase, ya que contiene las producciones almacenadas en el archivo de entrada.

Llegado el momento del analisis sintactico se llama al metodo analizar() para dar inicio a la logica del analisis. Se inicia desde la primera produccion y de forma recursiva se ingresa a los metodos (Simbolos no terminales) o se llama a la funcion matchToken(Simbolos terminales). Los metodos estan creados apartir de las producciones de la gramatica.

```
Ej.

private bool Type()
    {
        bool value = false;
        int indiceActual = tokenActual;

        if (matchToken("PALABRA_RESERVADA_INT"))
        {
            if (Type_hijo())
                return true;
        }
        if (matchToken("PALABRA_RESERVADA_DOUBLE"))
        {
            if (Type_hijo())
                return true;
        }
        if (matchToken("PALABRA_RESERVADA_BOOLEAN"))
        {
            if (Type_hijo())
                return true;
        }
        if (matchToken("PALABRA_RESERVADA_BOOL"))
        {
            if (Type_hijo())
                return true;
        }
        if (matchToken("PALABRA_RESERVADA_STRING"))
        {
            if (Type_hijo())
                return true;
        }
        if (matchToken("IDENTIFICADOR"))
        {
            if (Type_hijo())
                return true;
        }

        resetIndice(indiceActual);
        return value;
    }
```

Cuando el analizador detecta una produccion invalida regresa a la produccion inicial, guarda la posicion del token incorrecto, actualiza el index del analizador para pasar al siguiente Token y por ultimo vuelve a ingresar en la produccion inicial para recorrer el flujo nuevamente.


## Desarrollo 📌

El proyecto fue desarrollado en Visual Studio 2019 en el lenguaje C#.


## Autores ✒️

_Este proyecto fue realizado por:_

* **Kevin Barrientos**
* **Angel Jimenez**