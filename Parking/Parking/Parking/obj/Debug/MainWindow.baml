   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5	   	   B >Parking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Parking.ViewModelParking.ViewModel 6O      Parking.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5   	   @d2http://schemas.microsoft.com/expression/blend/2008      5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5   	   Iscm18clr-namespace:System.ComponentModel;assembly=WindowsBase      5	   	   5	viewmodelclr-namespace:Parking.ViewModel      6O   -   5          �Name$  	LogWindow��5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   
   $	��180��5   	   $	��375��6   $	��180��6(   $	��375��68   $	��180��6G   $	��375��6T   
������� J� InputBindings 5      �� 5   
    �� Gesture$	 Esc��6    �� Command 6#   �� *CloseLogWinCommand+ �� CommandParameter 5      ��  �� RelativeSource ��  �� Mode$ FindAncestor=�	 �� AncestorType"	 �.��5      � 
 � ColumnDefinitions
 5   
   �� 5      �� 5      �� 5      �� 5      $
��.05* �6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      $��0.75* �6   .+�5!   
   (� $��20��6    (� Stretch$ Uniform=�6$   $��5q�66   $��Center=�6A   $��Stretch=�5"      .��5#      �� $��Логін:��6   $
��Bold"�6'   y�69   �� *ConnectionWell+ �� UpdateSourceTrigger$ PropertyChanged=�5$      �� 5&   
   -   5         ��Name$ Login��5&      $��1��6!   $��2��61   $��5q�6F   
������� �� 	MaxLength$ 30��5'   &   $��Center=�65   $��Center=�5(   `    �� TextAlignment$ Center=�5)      $��0��6+   ��5(      �� *	UserLogin+ �� Mode$ TwoWay=�$ PropertyChanged=�y�5)   8   �� *ConnectionWell+$ PropertyChanged=�.��6�   5+      �� 5-   
   $��1��6   $��3��6!   $��5q�66   
�������$ 30��5/   '   $��Center=�66   $��Center=�50   W   $ Center=�51      $��0��6+   y�5.      �� *SqlEditVisability+$ PropertyChanged=���50      �� *NewServerName+$ PropertyChanged=�.��51   9   53      (� 55   
   $��1��6   $��20��6    $ Uniform=�60   $��5q�6B   $��Center=�6M   $��Stretch=�56      .��57      �� $��Пароль:��6   $
��Bold"�6(   y�6:   �� *ConnectionWell+$ PropertyChanged=�58      (� 5:   
   $��3��6   $��20��6'   $ Uniform=�67   $��5q�6I   $��Center=�6T   $��Stretch=�5;      .��5<      �� $2��,Введіть назву SQl серверу��6   $
��Bold"�6:   
���  ��y�5=      �� *SqlEditVisability+$ PropertyChanged=�5>      5?      G� 5B   
   -   5         G�Name$ passwordBox��5B      $��1��6,   $��2��6<   $��1��6P   $��5.2,5.4,0,5.2q�5C      
�������$
��Left=�6A    G� PasswordChar$ *��5D      $	��230��6(   $��1��64   $��Center=�6A   $��Stretch=�5E      $��Center=�62   y�5F      �� *ConnectionWell+$ PropertyChanged=��� 5G   
   -   5         ��Name$ plain��5G   #    �� Padding$
 10,0q�6   y�62   �� *ConnectionWell+$ PropertyChanged=�(� 5J   
   $��18��6   $ Uniform=�6"   $��2q�64   $��Right=�6?   $��2��6[   $��Center=�5K      5L      �� 5M   
   $��1��6   $��2��6!   $��Ok��6.   ��5N      �� *NewServerConnectionCommand+��5O      ��  �� ElementName$ 	LogWindow��y�5P      �� *SqlEditVisability+$ PropertyChanged=��� 5R   
   $��2��6   $��2��6   $��Ok��6/   
����  �
�������$��3��5S      y�5T      �� *ConnectionWell+$ PropertyChanged=�*  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5W      +  %Microsoft.Xaml.Behaviors.EventTrigger  5X         	EventName$ Click��6!   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5Y      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 6,   �� *ForCloseLogWinCommand+   CommandParameter 6V   �� $ 	LogWindow��  5Z       6,   �� *CheckPersoneCommand+ 6T   �� $ passwordBox��5[      5]      5_      5`      