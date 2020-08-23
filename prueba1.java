package quality;

public class AppValidaRut {

    public static void main(String[] args) {

        if(true){

            System.out.println("Rut Válido");//Este comando les permitirá imprimir por consola
        }else{
            System.out.println("Rut inválido");
        }
    }

    public static boolean validarRut(String rut) {

        boolean validacion = false;
        try {
            rut = rut.toUpperCase();
            rut = rut.replace(".", "");
            rut = rut.replace("-", "");
            int rutAux = Integer.parseInt(rut.substring(0, rut.length() - 1));
            char dv = rut.charAt(rut.length() - 1);
        } catch (java.lang.NumberFormatException e) {
        }
        return validacion;
    }
}