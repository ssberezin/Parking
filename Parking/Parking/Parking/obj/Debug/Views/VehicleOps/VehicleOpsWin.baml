   M S B A M L   `   `   `  ����B  >Parking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ParkingParking  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel 5   	   U/clr-namespace:Parking.ViewModel.VehicleTypesOps!Parking.ViewModel.VehicleTypesOps  6O   /clr-namespace:Parking.HelpesParking.Helpes  6�   Q-clr-namespace:Parking.ViewModel.DictionaryOpsParking.ViewModel.DictionaryOps  6�   6    0Parking.Views.VehicleTypesOps.VehicleTypesOpsWin   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   (helpesclr-namespace:Parking     5   	   Iscm18clr-namespace:System.ComponentModel;assembly=WindowsBase     5   	   Kvehicletypesops/clr-namespace:Parking.ViewModel.VehicleTypesOps     6O   0helpes1clr-namespace:Parking.Helpes     6�   Gdictionaryops-clr-namespace:Parking.ViewModel.DictionaryOps     6�   $	��300��5
   	   $	��250��6   $	��300��6(   $	��250��68   $	��300��6G   $	��250��6T     � WindowStartupLocation$  CenterScreen=�5   	    � Title 5	   	   �� *
WinTitle+ �� UpdateSourceTrigger$ PropertyChanged=� � 	Resources 5      +   %Parking.Helpes.NullToBooleanConverter   NullBoolConverter Win‌​dowsBaseX>clr-namespace:System.ComponentModel;assembly=Win‌​dowsBaseSystem.ComponentModel 5        SortedTypes �� Source �� SortDescriptions+  %System.ComponentModel.SortDescription   PropertyName�� 5   
   %�   &	        &	        5      6-   �� 5      Nscm>clr-namespace:System.ComponentModel;assembly=Win‌​dowsBase     5       5   7   �� *	SomeTypes+ 5        5      $ SomeName��6+   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5       �� 5!      �� 5#       � RowDefinitions 5%   
   �� 5&      $
��0.5* �6   �� 5'      �� 5(      �� 5)      $
��0.5* �6   .+�5+   
   �� -   5        	 ��Name$	 SomeName��5+      $��3��6$   $��5q�68   
 �� TextWrapping$

 Wrap=�5,   ^    �� 	IsEnabled .  �� TextAlignment$ Center=�5-   /   $��0��5.      ��5,      �� *SelectedType.SomeName+$ PropertyChanged=�5.      �� 5/   
   -   5         ��Name$
 List��5/      $��0��6!   $��3��61   $��1��6E   $��2��6R   $��5q�6c    �� SelectionMode$ Single=�52      $ �� IsSynchronizedWithCurrentItem$ FalseY�54      $��2��55      $��Disabled=�56      ��50      ��  �� Source# [  �� SelectedItem 51      �� *SelectedType+$ PropertyChanged=� �� SelectedValuePath 53      �� *SelectedType.SomeName+��57      �� 58      .R�59      ]� .+�5:      �� $��2q�6>   $��WrapWithOverflow=�5;   $    �� TextAlignment$
 Left=�5<   $   ��5:   $   �� *
SomeName+5<   $   5=      5?      5B      �� 5C   
   $��0��6   $��3��6!   $��Додати��6/   
��� � ���5D      �� *SaveDataCommand+��5E      ��  �� ElementName$ SomeName�� �� Path$
 Text�{�5F      �� $ SomeName��$ Text.Length�5G      �� 5J   
   $��1��6   $��3��6!   $��Видалити��6.   
����  ���6A   �� *DeleteDataCommand+��5K      �� $ SomeName��$
 Text�{�5L      �� *SelectedItem+ �� 	Converter# [  $
 List���� 5N   
   $��2��6   $��3��6!   $��Редагувати��6/   
��� �����5O      �� *EditDataCommand+��5P      �� $ SomeName��$
 Text�{�5Q      �� *SelectedItem+# [  $
 List��5S      5T      5U      