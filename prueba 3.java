/**
 *
 * @author Kevin
 */
public class Prueba extends javax.swing.JFrame {

    /**
     * Creates new form JFramePrincipal
     */
    public Prueba() {
        initComponents();
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    private void initComponents() {

        TFRuta = new javax.swing.JTextField();
        TFNombre = new javax.swing.JTextField();
        jLabel2 = new javax.swing.JLabel();
        BObtener = new javax.swing.JButton();
        jButton4 = new javax.swing.JButton();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);

        TFRuta.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                TFRutaActionPerformed(evt);
            }
        });

        BRuta.setText("Ruta");
        BRuta.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                BRutaActionPerformed(evt);
            }
        });   
    }

    private void BRutaActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_BRutaActionPerformed
       JFileChooser dialogo = new JFileChooser();
       
       File ficheroImagen;
       String rutaArchivo;
       int valor = dialogo.showOpenDialog(this);
       if(valor == JFileChooser.APPROVE_OPTION){
           ficheroImagen = dialogo.getSelectedFile();
           rutaArchivo = ficheroImagen.getPath();
           
           TFRuta.setText(rutaArchivo);
       }
    }
    
    
    
    public static void main(String args[]) {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(Lab1_1010617.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(Lab1_1010617.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(Lab1_1010617.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(Lab1_1010617.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>
        //</editor-fold>

        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new Lab1_1010617().setVisible(true);
            }
        });
    }

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton BGrabar;
    private javax.swing.JButton BObtener;
    private javax.swing.JButton BRuta;
    private javax.swing.JList LApellido;
    private javax.swing.JList LNombre;
    private javax.swing.JTextField TFNombre;
    private javax.swing.JTextField TFRuta;
    private javax.swing.JButton jButton4;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JScrollPane jScrollPane1;
    private javax.swing.JScrollPane jScrollPane2;
    // End of variables declaration//GEN-END:variables
}