# Fase 3 de Proyecto - Analisis Semántico

El repositorio corresponte a la tercera fase de proyecto de la clase de compiladores. Dicha dicha consta de la creación de un analizador semántico para la gramatica establecida. En nuestro caso se nos solicito realizar el analizador para minijava


## Elementos del Proyecto 📋

El proyecto consta de 4 partes fundamentales. La primera parte es "AnalizadorLexico", la cual se encarga de realizar un analisis léxico del archivo de entrada, ademas, crea una lista de tokens que se utilizan posteriormente en la parte de analisis sintáctico.

La segunda parte es el "AnalizadorSintactico", el cual es la segunda fase de proyecto y se encarga de validar el orden de la secuencia de tokens por lo que utiliza una logica de analisis sintacticos.

La tercera parte es la "TablaSimbolos", la tabla se encarga de llevar nuestros simbolos detectados y almacenar sus atributos. Se utiliza para validar la duplicidad o la asignación de valor.

Por último esta el "AnalizadorSemantico", este se encarga de evaluar la lógica del archivo de entrada y verificar que los simbolos tengan congruencia en sus atributos.

Para implementar un analizador semántico se necesitan de un concepto basico como lo es:

### Tabla de Simbolos
_Se encarga del control de los simbolos que se reconocen en la lógica del archivo de entrada, además de apoyar en la detección de errores como una asignación de un tipo de valor diferente al símbolo. Además almacena los atributos del símbolo para futuras comparaciones._



## Lógica del Proyecto ⌨️

Al inicio se debe cargar un archivo (La interfaz gráfica del programa permite acceder al explorador de archivos y seleccionar uno) para obtener el contenido que se desea analizar. Luego el programa carga las expresiones regulares a la estructura Regex para generar las reglas de análisis.

Al momento de tener lista la estructura Regex se procede a realizar el analisis lexico del archivo, al finalizar el analisis se genera una lista de tokens de salida. Esta lista sirve de base para la siguiente fase, ya que contiene las producciones almacenadas en el archivo de entrada.

Llegado el momento del analisis sintactico se llama al metodo analizar() para dar inicio a la logica del analisis. Se inicia desde la primera produccion y de forma recursiva se ingresa a los metodos (Simbolos no terminales) o se valida con la tabla de simbolos para verificar la validez de la acción. La tabla de estados esta construida apartir de las producciones de la gramatica.

```
Ej.

dTablaAnalisis.Add(new Validacion("SIMBOLO_FINAL_ARCHIVO", 1), new Accion("Aceptar", default));
```

Cuando el analizador detecta una produccion invalida regresa a la produccion inicial, guarda la posicion del token incorrecto, actualiza el index del analizador para pasar al siguiente Token y por ultimo vuelve a ingresar en la produccion inicial para recorrer el flujo nuevamente.

Para finalizar realiza el análisis semántico y con ayuda de la tabla de símbolos, verifica las asignaciones, operaciones y llamados que se realizan dentro de la lógica del archivo de entrada para determinar la validez de la acción. Además, cada vez que se realiza una operación actualiza el valor de la variable en la tabla de símbolos para así llevar un control de los valores finales.

## Gramatica ⌨️

La gramática que se utilizó durante el proyecto es:

```
GRAMATICA G'

[0] <S'> ::= <S> $
[1] <S> ::= <Program>
[2] <Program> ::= <Decl> <Program1>
[3] <Program1> ::= <Decl> <Program1>
[4] <Program1> ::= Epsilon
[5] <Decl> ::= <VariableDecl>
[6] <Decl> ::= <FunctionDecl>
[7] <Decl> ::= <ConstDecl>
[8] <Decl> ::= <ClassDecl>
[9] <Decl> ::= <InterfaceDecl>
[10] <VariableDecl> ::= <Variable> ;
[11] <Variable> ::= <Type> identificador
[12] <ConstDecl> ::= static <ConstType> identificador ;
[13] <ConstType> ::= int
[14] <ConstType> ::= double
[15] <ConstType> ::= boolean
[16] <ConstType> ::= string
[17] <Type> ::= int
[18] <Type> ::= double
[19] <Type> ::= boolean
[20] <Type> ::= string
[21] <Type> ::= identificador
[22] <Type> ::= <Type> []
[23] <FunctionDecl> ::= <Type> identificador ( <Formals> ) <StmtBlock>
[24] <FunctionDecl> ::= void identificador ( <Formals> ) <StmtBlock>
[25] <Formals> ::= <Variable> , <Formals>
[26] <Formals> ::= <Variable>
[27] <Extends1> ::= extends identificador
[28] <Extends1> ::= Epsilon
[29] <Identificador1> ::= identificador
[30] <Identificador1> ::= identificador <Identificador1>
[31] <Implements1> ::= implements <Identificador1> ,
[32] <Implements1> ::= Epsilon
[33] <Field1> ::= <Field> <Field1>
[34] <Field1> ::= Epsilon
[35] <ClassDecl> ::= class identificador <Extends1> <Implements1> { <Field1> }
[36] <Field> ::= <VariableDecl>
[37] <Field> ::= <FunctionDecl>
[38] <Field> ::= <ConstDecl>
[39] <Prototype1> ::= <Prototype> <Prototype1>
[40] <Prototype1> ::= Epsilon
[41] <InterfaceDecl> ::= interface identificador { <Prototype1> }
[42] <Prototype> ::= <Type> identificador ( <Formals> ) ;
[43] <Prototype> ::= void identificador ( <Formals> ) ;
[44] <VariableDecl1> ::= <VariableDecl> <VariableDecl1>
[45] <VariableDecl1> ::= Epsilon
[46] <ConstDecl1> ::= <ConstDecl> <ConstDecl1>
[47] <ConstDecl1> ::= Epsilon
[48] <Stmt1> ::= <Stmt> <Stmt1>
[49] <Stmt1> ::= Epsilon
[50] <StmtBlock> ::= { <VariableDecl1> <ConstDecl1> <Stmt1> }
[51] <Expr1> ::= <Expr>
[52] <Expr1> ::= Epsilon
[53] <Stmt> ::= <Expr1> ;
[54] <Stmt> ::= <IfStmt>
[55] <Stmt> ::= <WhileStmt>
[56] <Stmt> ::= <ForStmt>
[57] <Stmt> ::= <BreakStmt>
[58] <Stmt> ::= <ReturnStmt>
[59] <Stmt> ::= <PrintStmt>
[60] <Stmt> ::= <StmtBlock>
[61] <Else1> ::= else <Stmt>
[62] <Else1> ::= Epsilon
[63] <IfStmt> ::= if ( <Expr> ) <Stmt> <Else1>
[64] <WhileStmt> ::= while ( <Expr> ) <Stmt>
[65] <ForStmt> ::= for ( <Expr> ; <Expr> ; <Expr> ) <Stmt>
[66] <ReturnStmt> ::= return <Expr> ;
[67] <BreakStmt> ::= break ;
[68] <Expr2> ::= <Expr> , <Expr2>
[69] <Expr2> ::= <Expr>
[70] <PrintStmt> ::= System . out . println ( <Expr2> ) ;
[71] <Expr> ::= <LValue> = <Expr>
[72] <Expr> ::= <Constant>
[73] <Expr> ::= <LValue>
[74] <Expr> ::= this
[75] <Expr> ::= ( <Expr> )
[76] <Expr> ::= <Expr> - <Expr>
[77] <Expr> ::= <Expr> / <Expr>
[78] <Expr> ::= <Expr> % <Expr>
[79] <Expr> ::= - <Expr>
[80] <Expr> ::= <Expr> > <Expr>
[81] <Expr> ::= <Expr> >= <Expr>
[82] <Expr> ::= <Expr> != <Expr>
[83] <Expr> ::= <Expr> || <Expr>
[84] <Expr> ::= ! <Expr>
[85] <Expr> ::= New ( identificador )
[86] <LValue> ::= identificador
[87] <LValue> ::= <Expr> . identificador
[88] <Constant> ::= intConstant
[89] <Constant> ::= doubleConstant
[90] <Constant> ::= booleanConstant
[91] <Constant> ::= stringConstant
[92] <Constant> ::= null

```

## Desarrollo 📌

El proyecto fue desarrollado en Visual Studio 2019 en el lenguaje C#.


## Autores ✒️

_Este proyecto fue realizado por:_

* **Kevin Barrientos**
* **Angel Jimenez**