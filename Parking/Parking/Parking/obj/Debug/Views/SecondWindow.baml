   M S B A M L   `   `   `  ����B  >Parking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Parking.ViewsParking.Views  5   	   5clr-namespace:Parking.ViewModelParking.ViewModel  5   	   /clr-namespace:Parking.HelpesParking.Helpes  5	   	        Parking.Views.SecondWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   'localclr-namespace:Parking.Views  5   	   /	viewmodelclr-namespace:Parking.ViewModel  5   	   )helpesclr-namespace:Parking.Helpes  5	   	     � Title$G  AСтан зайнятості паркувальних місць��5   	   $	��550��64   $	��800��6A    � 	Resources 5      *   $Parking.Helpes.BoolToStringConverter   boolToStringConverter+   %Parking.Helpes.BoolToStringConverter2  boolToStringConverter22   ,Parking.Helpes.BoolToBrushConverterForStatus ! boolToBrushConverterForStatus1   +Parking.Helpes.BoolToBrushConverterForInOut   boolToBrushConverterForInOut�� 5   
   %t   &	        &	      &	 &     &	 9       5      6+     5      6,     5      63     5      62   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��1.5* �6   �� 5       � RowDefinitions 5   
   �� 5      $��0.06* �6   �� 5      .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��3��6.   .+�5       o� $��14(�6   $��1q�6"   $	��Top=�6/   $��30��6G   $��0,0,0,1q�6S   .@�5!      m� $��
Звіти��6   $	��Top=�6,   .@�5#      m� $-��'Звіт за власниками ТЗ��6    m� Command 6>   �� *CallOwnersReportCommand+m� 5$      $��Виручка каси��6    66   �� *CallMoneyMoveReportCommand+5&      m� 5+      $��Сервіс��6   $	��Top=�6.   .@�5.      m� $0��*Данні про підприємство��6    6?   �� *CallCompanyInfoWindowCommand+m� 5/      $��Персонал��6   $	��Top=�62   .@�52      m� $)��#Список працівників��6#   $	��Top=�53   #    54   #   �� *CallStuffListCommand+55      m� 56      $��Словники��6   $	��Top=�62   .@�58      m� $��Типи ТЗ��6#   $	��Top=�59   #    5:   #   �� *CallVehTypeOpsCommand+m� 5;      $��Кольори��6#   $	��Top=�5<   #    5=   #   �� *CallColorOpsCommand+5>      5?      5U      �� 5V      $��20,0,0,0q�67   $��Center=�6I   $��Center=�6f   $
��Bold"�6�   
��� � ���6   �� *ReportMessage+6�   5W      �� 5X   
   $��1��6   $U��OІнформація про траспортний засіб і клієнта��6%   $��Center=�6W   $��Center=�6t   $
��Bold"�6�   �� 5Z   
   $��1��6   $��1��6"   $��6q�62   y�6=   �� *
VisaBility+ �� UpdateSourceTrigger$ PropertyChanged=�.+�5[      �� 
�������$��2q�6)   ._�5\      �� 
���z���.+�5]      �� .+�5^      �� $��Власник ��6$   $
��Bold"�64   $��Center=�6F   �� 5_      $
��Bold"�6{   
����  �$��16(�6�   $��Center=�6�   ��6$   �� *" CurrentRecord.SomeClient.OrgName+$ PropertyChanged=�6�   5`      �� 5a      .+�5b      �� $��Водій ��6$   $
��Bold"�62   $��Center=�6D   �� 5d      $ 
Horizontal=�6%   $��Center=�6>   .+�5e      �� $
��Bold"�6�   
����  �$��16(�6�   $��Center=�6�   ��6(   �� *%#CurrentRecord.SomePerson.SecondName+$ PropertyChanged=�6�   �� 5f      $�� ��6(   �� 5g      $
��Bold"�6�   
����  �$��16(�6�   $��Center=�6�   ��6(   �� *$"CurrentRecord.SomePerson.FirstName+$ PropertyChanged=�6�   �� 5h      $�� ��6(   �� 5i      $
��Bold"�6�   
����  �$��16(�6�   $��Center=�6�   ��6(   �� *%#CurrentRecord.SomePerson.Patronimic+$ PropertyChanged=�6�   5j      5k      �� 5l      $��Center=�6!   .+�5m      �� $��№ телефона ��6$   $
��Bold"�67   $��Center=�6I   �� 5n      $
��Bold"�6|   
��� � �$��16(�6�   $��Center=�6�   ��6$   �� *" CurrentRecord.SomeContacts.Phone+$ PropertyChanged=�6�   5o      5p      5q      �� 5r      
�������$��1q�6)   ._�5s      �� .+�5t      �� $��Center=�6!   .+�5u      �� $"��Довірена особа ��6$   $
��Bold"�6;   $��Center=�6M   �� 5w      $ 
Horizontal=�6%   $��Center=�6>   .+�5x      �� $
��Bold"�6�   
����  �$��16(�6�   $��Center=�6�   ��6(   �� *(&CurrentRecord.TrustedPerson.SecondName+$ PropertyChanged=�6�   �� 5y      $�� ��6(   �� 5z      $
��Bold"�6�   
����  �$��16(�6�   $��Center=�6�   ��6(   �� *'%CurrentRecord.TrustedPerson.FirstName+$ PropertyChanged=�6�   �� 5{      $�� ��6(   �� 5|      $
��Bold"�6�   
����  �$��16(�6�   $��Center=�6�   ��6(   �� *(&CurrentRecord.TrustedPerson.Patronimic+$ PropertyChanged=�6�   5}      5~      �� 5      $��Center=�6!   .+�5�      �� $��№ телефона ��6$   $
��Bold"�67   $��Center=�6I   �� 5�      $
��Bold"�6y   
����  �$��20(�6�   ��6$   �� * CurrentRecord.TrContacts.Phone+$ PropertyChanged=�6�   5�      5�      5�      �� 5�      
�������
����ح�$��2q�6?   ._�5�      �� $��Center=�6   .+�5�      �� $-��'Реэстраційний номер: ��6    $
��Bold"�6=   $��Center=�6O   �� 5�      $
��Bold"�6z   
��� � �$��20(�6�   ��6    �� *%#CurrentRecord.SomeVehicle.RegNumber+$ PropertyChanged=�6�   5�      5�      �� 5�      $��Center=�6   .+�5�      �� $��Колір ТЗ ��6   $
��Bold"�6-   $��Center=�6?   �� 5�      $
��Bold"�6s   
���   �$��16(�6�   $��Center=�6�   ��6   �� *" CurrentRecord.VehColor.ColorName+$ PropertyChanged=�6�   5�      �� 5�      
�������$��2q�6)   ._�5�      �� $��Center=�6   .+�5�      �� $��ТИП ТЗ ��6    $
��Bold"�6/   $��Center=�6A   �� 5�      $
��Bold"�6~   
��� � �$��20(�6�   $��Center=�6�   ��6    �� *(&CurrentRecord.SomeVehicleType.TypeName+$ PropertyChanged=�6�   5�      5�      �� 5�      $��Center=�6   .+�5�      �� $>��8Послуга паркування дійсна до : ��6   $
��Bold"�6C   $��Center=�6U   �� 5�      $
��Bold"�6[   
����  �$��20(�6   $��Center=�6�   ��6   �� *
DeadLine+$ PropertyChanged=�6�   5�      �� 5�      
�������$��2q�6)   ._�5�      �� $��Center=�6   .+�5�      �� $(��"Прострочено чи ні? ��6    $
��Bold"�6;   $��Center=�6M   �� 5�      $
��Bold"�6d   
���  ��$��20(�6�   $��Center=�6�   ��6    �� *OutOfDeadLine+$ PropertyChanged=�6�   5�      5�      5�      &   System.Windows.Controls.DataGrid  5�   
   -   5          Name$ 
GridOrders��5�      $��1��6G   $��0��6T   $��3q�5�         AutoGenerateColumns .  	   SelectionMode$	 Single=�5�   /   
   
IsReadOnly
 . $	��Top=�5�   (      	RowHeight$ 30Q�5�      $��15(�6"   ��5�   '   �� *	Records+ �� SelectedItem 5�      �� *SelectedRecord+ J� InputBindings 5�      d� 5�       d� MouseAction$ LeftDoubleClicke�5�       �� Command 5�      �� *EditparkPlaceCommand+   RowStyle 5�      �� 5�       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5�      ��  �� Property � �� �� Value 5�      �� *SomeParkingPlace.FreeStatus+ �� 	Converter# [ �� 5�       � �� 5�      �� *SomeParkingPlace.Released+# [ 5�       � ContextMenu 5�      �� 5�      .@�5�      m� $6��0Зайняти паркувальне місце��6    m� CommandParameter 5�      ��  �� ElementName$ 
GridOrders�� �� Path$ SelectedItem� 5�      �� *EditOrderCommand+m� 5�      $:��4Звільнити паркувальне місце��6    5�      �� $ 
GridOrders��$ SelectedItem� 5�      �� *ClientInfoCommand+m� 5�      $��Редагувати��6    5�      �� *EditparkPlaceCommand+m� 5�      $'��!Роздрук квитанції��6    5�      �� *CallClientOrdersInfoCommand+5�         Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Width5	  /System.Windows.Controls.DataGridLengthConverter$ 0.355*	 6%      Header$ № місця��6j   1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 64   �� *" SomeParkingPlace.ParkPlaceNumber+6j     5�      $
 0.5*	 6%   $ Статус��6�    62   �� *SomeParkingPlace.FreeStatus+# [  $ PropertyChanged=�6�     5�      $ 0.35*	 6%   $ Заїхав/виїхав��6�    64   �� *SomeParkingPlace.Released+# [ $ PropertyChanged=�6�   5�      5�      5�      