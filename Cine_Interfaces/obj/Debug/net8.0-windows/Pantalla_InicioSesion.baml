   M S B A M L   `   `   `  ����  Cine_Interfaces+    %Cine_Interfaces.Pantalla_InicioSesion   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Iniciar Sesión��5   	   $	��720��6    $
��1080��6-    � WindowStartupLocation$ CenterScreen=�5   	   .��5      � ��5   
   �� 5       �� ImageSource$ Images/fondo.jpeg��6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��3* �6   .+�5   
   �� $��Iniciar Sesión��6   $��Center=�5      $��Bottom=�61   $��28(�5      $
��Bold"�6"   $��0��64   
�������$��Bahnschrift)�6T   �� 5   
    �� Orientation$ Vertical=�6   $��Center=�6,   $	��Top=�6I   $��1��6a   $	��300��6n   $��	0,192,0,0q�6z   .+�5      �� $��Nombre de Usuario:��6   $
��Left=�5      $��16(�63   $��0,10,0,5q�6A   
�������$��Bahnschrift)�6f   �� 5      -   5        $��UsernameField��5      $��0,0,0,20q�6+   �� 5      $��Contraseña:��6   $
��Left=�5      $��16(�63   $��0,10,0,5q�6A   
�������$��Bahnschrift)�6f   G� 5      -   5        $��PasswordField��5      $��0,0,0,10q�6/   �� 5"      -   5        $+��%¿No tienes cuenta? Regístrate aquí��5"      $��Right=�5#      
����ح�$��14(�5$      $��0,5,0,20q�6&   $��	Underline{�5%       � Cursor$
 Hand��64   6B   �� 5(      $ 
Horizontal=�6   $��Center=�62   .+�5)      �� -   5        $��Cancelar��5)      $	��100��6,   $��5q�68   6C   �� 5*      -   5        $��Aceptar��5*      $	��100��6+   $��5q�67   6B   5+      5,      �� 5/   
   -   5        $��Images/volver.png��5/      $��Right=�50      $��Bottom=�6,   $��	0,0,10,10q�51      $��50��6#   $��50��6.   $
 Hand��52       J� RenderTransformOrigin$ -0.84,4.381&�6A   $��1��6e   53      54      