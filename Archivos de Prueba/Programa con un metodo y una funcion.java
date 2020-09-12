/*
    El archivo debe tomarse como valido ya que no presenta errores
*/
Class formulario;

int numero;
double decimal;
boolean validator;
string cadena;
int[] numeros;
int[] aux;

public formulario()
    numero = 0;
    decimal = 2.90;
    validator = true;
    cadena = "Es una cadena";
    numeros = New(int);
    aux = New(int);

void primerMetodo()
    decimal = numero + decimal ;
    numeros = funcionNueva(1, numeros);

int[] funcionNueva(int enteroNuevo, int[] decimalNuevo)

    for( i=0; i<10; i+1)
        aux[i] = aux[i].value + 5;
    return aux;