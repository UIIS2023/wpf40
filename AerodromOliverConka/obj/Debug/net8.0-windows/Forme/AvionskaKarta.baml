   M S B A M L   `   `   `  ����N  JAerodromOliverConka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:AerodromOliverConka.FormeAerodromOliverConka.Forme  5   	   -    'AerodromOliverConka.Forme.AvionskaKarta   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:AerodromOliverConka.Forme     5   	     � Title$  AvionskaKarta��5   	   $	��450��6   $	��800��6,   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��367* �6   �� 5      $	��67* �6   .+�5   
   �� $��
Cena karte��6   $
��Left=�6%   $��	88,47,0,0q�6@   $	��Top=�6S   $��16(�6k   $��35��6y   �� 5   
   $��Datum kupovine��6   $
��Left=�6)   $��	88,99,0,0q�6D   $	��Top=�6W   $��16(�6o   $��35��6}   �� 5   
   $��Check In��6   $
��Left=�6#   $��
88,157,0,0q�6>   $	��Top=�6R   $��16(�6j   $��35��6x   �� 5   
   $��Let ID��6   $
��Left=�6!   $��
88,212,0,0q�6<   $	��Top=�6P   $��16(�6h   $��36��6v   �� 5   
   $��Klasa ID��6   $
��Left=�6#   $��
88,264,0,0q�6>   $	��Top=�6R    J� RenderTransformOrigin$ 0.554,-0.892&�6j   $��16(�6�   $��35��6�   �� 5   
   $��	Putnik ID��6   $
��Left=�6$   $��
88,314,0,0q�6?   $	��Top=�6S   $��16(�6k   $��35��6y   �� 5   
   $��Broj sedista��6   $
��Left=�6'   $��
88,366,0,0q�6B   $	��Top=�6V   $��16(�6n   $��35��6|   $��2��6�   �� 5   
   -   5         ��Name$ txtCena��5      $
��Left=�6#   $��
264,47,0,0q�6>    �� TextWrapping$
 Wrap=�6R   $	��Top=�6f   $	��120��6~   $��16(�6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dtpDatum��5      $
��Left=�6'   $��
264,99,0,0q�6B   $	��Top=�6V   $��35��6n   $��16(�6z   �� 5   
   -   5         ��Name$ cmbLetId��5      $
��Left=�6%   $��264,212,0,0q�6@   $	��Top=�6U   $	��120��6m   �� 5   
   -   5        $ 
cmbKlasaId��5      $
��Left=�6'   $��264,260,0,0q�6B   $	��Top=�6W   $	��120��6o   �� 5   
   -   5        $ cmbPutnikID��5      $
��Left=�6(   $��264,321,0,0q�6C   $	��Top=�6X   $	��120��6p   �� 5   
   -   5        $ txtBrojSedista��5      $
��Left=�6*   $��266,367,0,0q�6E   $
 Wrap=�6Z   $	��Top=�6n   $	��120��6�   $��16(�6�   $��2��6�   �� 5   
   -   5         ��Name$ chkCheck��5      $�� ��6%   $
��Left=�60   $��310,174,0,0q�6K   $	��Top=�6`   �� 5   
   -   5         ��Name$ 
btnSacuvaj��5      $��Sacuvaj��6%   $
��Left=�67   $��558,102,0,0q�6R   $	��Top=�6g   $ -0.607,0.847&�6   $��49��6�   $	��110��6�   $��16(�6�   6�   �� 5   
   -	   5        $ 	btnOtkazi��5      $��Otkazi��6$   $
��Left=�65   $��558,233,0,0q�6P   $	��Top=�6e   $ -1.104,-0.766&�6}   $��49��6�   $	��110��6�   $��16(�6�   6�   5      5       