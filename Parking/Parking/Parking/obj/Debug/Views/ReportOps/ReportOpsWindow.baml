   M S B A M L   `   `   `  ����B  >Parking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Parking.Views.ReportOpsParking.Views.ReportOps  5   	   /clr-namespace:Parking.HelpesParking.Helpes  5   	   /clr-namespace:Parking.HelpesParking.Helpes  5	   	   I)clr-namespace:Parking.ViewModel.ReportOpsParking.ViewModel.ReportOps  5
   	   -    'Parking.Views.ReportOps.ReportOpsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Parking.Views.ReportOps     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   $wclr-namespace:Parking.Helpes  5   	   )helpesclr-namespace:Parking.Helpes  5	   	   9	reportops)clr-namespace:Parking.ViewModel.ReportOps  5
   	   X TXceed.Wpf.Toolkit, Version=4.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f47wpfTool)http://schemas.xceed.com/wpf/xaml/toolkit  6E     � Title$  	ReportOps��5   	   $	��450��6   $
��1200��6(    � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5      *   $Parking.Helpes.BoolToStringConverter   boolToStringConverter+   %Parking.Helpes.BoolToStringConverter2  boolToStringConverter22   ,Parking.Helpes.BoolToBrushConverterForStatus ! boolToBrushConverterForStatus1   +Parking.Helpes.BoolToBrushConverterForInOut   boolToBrushConverterForInOut  HeaderTemplate �� TextAlignment=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value�� 5   
   %x  &	        &	      &	 &     &	 9     &	 L     '  �     7    5      6+     5      6,     5      63     5      62   �� 5      .R�5      �� $
��Wrap=�6-   $ Center=�6B   ��6   �� 6B   5      �� 5      "  6   .�5      ��  � ��$ Center=�6?   �� 5       � ��$ 2q�6,   �� 5       � ��8   64   5      5        J� InputBindings 5"      �� 5#   
    �� Gesture$ Ctrl+F��6   	 �� Command	 6&   �� *CallFindCommand+
 �� CommandParameter
 5$      �� *DataContext+ �� RelativeSource ��  �� AncestorType" �.��5(      �� ���	#FF9BECE6$��3q�6&   ._�5)   
   �  � ColumnDefinitions 5*      �� 5+      $��0.44*  �6#   �� 5,      $��0.44*  �6#   �� 5-      .+�5/      �� ���	#FF9BECE6$��2q�6.   ._�50      �  � RowDefinitions 51      �� 52      $��0.06* �6(   �� 53      .+�55      ��  �� Orientation$ 
Horizontal=�6!   .+�56      �� -   5         ��Name$ Filter1��56   #   $�� Фільтри ��64   $
��Left=�6H   $��Bottom=�6j   $
��Bold"�57      $��0,0,0,3q�61   � 58      $
��Left=�59        � 	StaysOpen . $��Slide=�5<   !   $��1,3,5,3q�68    � AllowsTransparency .  � PlacementTarget 58       ��  �� ElementName$ Filter1����5:       �� *	IsChecked+$ Filter1��.��5>      �� 
���   �$��1q�69   ._�5?   "   �� 
�������.+�5@   &   �� $��0,5,0,0q�61   .+�5A   *   �� $�� Піб клієнта ��64   $��Center=�6L   $��Center=�6g   $
��Bold"�5B   7    � ToolTip$t nМожна щось одне: прізвище. ім'я чи по батькові. Більше шансів.��6I    d� 	IsChecked 5C   7   �� *Filter1.CheckArg1+ �� UpdateSourceTrigger$ PropertyChanged=��� 5D   *   $��3q�67   $��Center=�6B   $ 
Horizontal=�6]   .+�5E   .   �� $	��5,2q�6~   $	��240��5F   1    �� 	MaxLength$ 28��6=   $I CЩось одне: Ім'я, прізвище, по батькові��5G   1   ��5E   7   �� *Filter1.StrArg1+$ PropertyChanged=�5G   1   5H   +   �� 5I   *   $3��- За статусом паркомісця  ��64   $
��Bold"�6U   $��Center=�6g   $��Center=�6�   �� 5J   *   $ 
Horizontal=�65   $��5,5,0,0q�6O   .+�5K   .   	�  	� 	GroupName$ A��6:   $��Вільне  ��6H   $
��Bold"�6[   
����  �$��Center=�6�    5L   8   �� *Filter1.CheckArg2+$ PropertyChanged=�	� 5M   .   $ A��6:   $��Зайняте  ��6H   $
��Bold"�6\   
����  �$��Center=�6�    5N   8   �� *Filter1.CheckArg3+$ PropertyChanged=�	� 5O   .   $ A��6:   $�� Всі��6H   $
��Bold"�6X   
����  �$��Center=�6|    5P   7   �� *Filter1.CheckArg4+$ PropertyChanged=�5Q   +   �� 5U   *   $ 
Horizontal=�65   $��3q�6N   .+�5V   .   �� $��	 від  ��68   $
��Bold"�6F   $��Center=�6X   $��Center=�6u   (  "System.Windows.Controls.DatePicker  5W   .      IsDropDownOpen .     SelectedDateFormat$ Short=�6Q   $��Center=�5X   1   $��80��6O   $��80��6Z   $��Center=�5[   1      DisplayDateEnd 5Y   2   �� *EndHistoryDate+$ PropertyChanged=�   SelectedDate 5Z   2   �� *StartHistoryDate+$ PropertyChanged=�5[   1   �� 5\   .   $�� до  ��68   $
��Bold"�6E   $��Center=�6W   $��Center=�6t     5]   .    .  $ Short=�6P   $��Center=�5^   1   $��80��6N   $��80��6Y   $��Center=�5a   1      DisplayDateStart 5_   1   �� *StartHistoryDate+$ PropertyChanged=� 5`   1   �� *EndHistoryDate+$ PropertyChanged=� �� Mode$ TwoWay=�5a   1   5b   +   5c   '   �� 5e   &   $+��%Застосувати фільтри��6-   $��Stretch=�6L   ���	#FF7E4A00$��3q�5g   >   $��25��6I   $	��270��6X   ��5f   %   �� *CallFilter1Command+5g   X   �� 5h   &   $%��Очистити фільтри��6-   $��3q�6H   $��25��6S   $	��270��6b   $��Stretch=�5i   M   
��� �����6%   �� *ClearFilter1Command+5j   %   5k   #   5l      5m      �� 5p      $+��% Показати боржників ��6"   
���  ��$
��Left=�6R   $��Center=�5q      $
��Bold"�66   $��5,0,0,0q�6H   ��5r   $   �� *FindDeptorsCommand+5u      &   System.Windows.Controls.DataGrid  5v      -   5           Name$  
GridOrders��5v      $��1��63   $��3q�5y      !   AutoGenerateColumns! .  "   SelectionMode$" Single=�5z   ;   #   
IsReadOnly# . $	��Top=�5{   4   $   	RowHeight$$ 30Q�5|      $��14(�6.   ��5w      �� *OwnerRecords+% �� SelectedItem% 5x      �� *SelectedRecord+&   Columns& 5      0  *System.Windows.Controls.DataGridTextColumn  5�      ,	  &System.Windows.Controls.DataGridColumn' 	  Width5
  /System.Windows.Controls.DataGridLengthConverter$' 0.82*
 61   ( 	  Header$( Власник��6�   1  +System.Windows.Controls.DataGridBoundColumn)   Binding) 6@   �� *	OwnerName+$ PropertyChanged=�6�     5�      $' 0.32*
 61   $( Сплачено до��6�   ) 6@   �� *MaxDeadLine+* �� StringFormat$* {0:dd.MM.yy }��$ PropertyChanged=�6�   5�      5�      5�      �� 5�      ���	#FF9BECE6$��2q�6.   $��1��6B   ._�5�      �  5�      �� 5�      $��0.06* �6(   �� 5�      .+�5�      �� $ 
Horizontal=�6!   .+�5�      �� -   5        $ Filter2��5�   #   $�� Фільтри ��64   $
��Left=�6H   $��Bottom=�5�   #   $
��Bold"�5�   #   $��0,0,0,3q�65   {�5�   >   �� *EnableFilter2+$ PropertyChanged=�5�   5   � 5�      $
��Left=�5�        . $��Slide=�5�   !   $��1,3,5,3q�68    .  5�       �� $ Filter2����5�       �� *	IsChecked+$ Filter2��.��5�      �� 
���   �$��1q�69   ._�5�   "   �� 
�������.+�5�   &   �� -   5        $ Check2_1��5�   /   $8��2 Номер паркувального місця ��6B   $��Center=�6h   $
��Bold"�5�   3   $��5q�5�   �    63   �� *Filter2.CheckArg1+$ PropertyChanged=�6�   �� 5�   &   -   5        + ��Name$+ 
ColorCombo��5�   /   $	��185��6D   $��Center=�6Q   $��0,0,0,20q�6l   , �� SelectedIndex$, 0��5�   *   - �� SelectedValuePath$- 	ColorName��5�   -   $��Center=�5�   *   . �� DisplayMemberPath$. PPlace.ParkPlaceNumber��5�   *   
�������$��14(�5�   *   {�5�   <   �� $ Check2_1��/ �� Path$/ 	IsChecked���5�   *   �� *ParPlaceRecords+$ PropertyChanged=�0 �� SelectedValue0 5�   *   �� *Filter2.IntArg1+$ OneWay=�5�   '   �� 5�   &   $ 
Horizontal=�61   $��5,5,0,0q�6K   .+�5�   *   �� -   5        $ Check2_2��5�   3   $�� Статсус ��6F   $��Center=�6Z   $
��Bold"�5�   3   $t nМожна щось одне: прізвище. ім'я чи по батькові. Більше шансів.��6E    5�   3   �� *Filter2.CheckArg2+$ PropertyChanged=��� 5�   *   $ 
Horizontal=�65   {�6N   �� $ Check2_1��$/ 	IsChecked�.+�5�   .   	� $ С��6:   $��Заїхав  ��6H   $
��Bold"�6[   
����  �$��Center=�6�    5�   8   �� *Filter2.CheckArg3+$ PropertyChanged=�	� 5�   .   $ С��6:   $��Виїхав��6H   $
��Bold"�6Z   
����  �$��Center=�6~    5�   7   �� *Filter2.CheckArg4+$ PropertyChanged=�5�   +   5�   '   �� 5�   &   $+��%Застосувати фільтри��6-   $��Stretch=�6L   ���	#FF7E4A00$��3q�5�   >   $��25��6I   $	��270��6X   ��5�   %   �� *CallFilter2Command+5�   X   �� 5�   &   $%��Очистити фільтри��6-   $��3q�6H   $��25��6S   $	��270��6b   $��Stretch=�5�   M   
��� �����6%   �� *ClearFilter2Command+5�   %   5�   #   5�      5�      5�        5�      -   5        $  	PPRecords��5�      $��1��62   $��9,3,-4,0q�5�      ! .  $" Single=�5�   ;   # . $	��Top=�5�   4   $$ 30Q�5�      $��14(�6.   ��5�      �� *ParPlaceRecords+$ PropertyChanged=�% 5�       �� *ParPlaceSelecteRecord+$ PropertyChanged=� 5�      d� 5�      1 d� MouseAction$1 LeftDoubleClicke�6+   	 5�   *   �� *EditparkPlaceCommand+2   RowStyle2 5�      �� 5�      )  #System.Windows.Controls.DataGridRow"  6$   .�5�   "   ��  � �� 5�      �� *PPlace.FreeStatus+3 �� 	Converter#3 [ $ PropertyChanged=��� 5�   "    � �� 5�      �� *PPlace.Released+#3 [ $ PropertyChanged=�5�      & 5�        5�      $' 0.32*
 61   $ ( № парко. місця��6�   ) 6@   �� *PPlace.ParkPlaceNumber+$ PropertyChanged=�6�     5�      $' 0.32*
 61   $( Статус��6�   ) 6@   �� *PPlace.FreeStatus+$ PropertyChanged=�#3 [  6�     5�      $' 0.32*
 61   $( Виїхав/заїхав��6�   ) 6@   �� *PPlace.Released+$ PropertyChanged=�#3 [ 6�   5�      5�      5�      �� 5�      ���	#FF9BECE6$��2q�6.   $��2��6B   ._�5�      �  5�      �� 5�      $��0.06* �6(   �� 5�      .+�5�      �� $
��Left=�6"   $ 
Horizontal=�6=   .+�5�      �� $ 
Horizontal=�6%   $��Center=�6>   .+�5�      �� -   5        $ Filter3��5�   '   $�� Фільтри ��68   $
��Left=�6L   $��Bottom=�5�   '   $
��Bold"�5�   '   $��0,0,0,3q�69   {�5�   B   �� *EnableFilter3+$ PropertyChanged=�5�   9   � 5�      $
��Left=�5�        . $��Slide=�5�   !   $��1,3,5,3q�68    .  5�   $   �� $ Filter3����5�       �� *	IsChecked+$ Filter3��.��5�   "   �� 
���   �$��1q�6=   ._�5�   &   �� 
�������.+�5�   *   �� .+�5  .   �� -	   5        $ Check3_1��5  7   $ ��Держ.номер ТЗ  ��6J   $��Center=�6d   $
��Bold"�5  /   $��0,3,0,0q�6A    5  /   �� *Filter3.CheckArg1+$ TwoWay=�$ PropertyChanged=��� 5  .   $	��5,5q�5  x   $	��170��5  1   $ 8��6=   {�5  7   �� $ Check3_1��$/ 	IsChecked���5  1   �� *Filter3.StrArg1+$ PropertyChanged=�5  =   5  +   �� 5	  *   $��Center=�65   .+�5  .   �� -
   5        $ Check3_2��5  7   $*��$Дата виїзду/заїзду  ��5  3   $��Center=�6R   $��Center=�6m   $
��Bold"�5  3   $��0,5,0,0q�6E    5  3   �� *Filter3.CheckArg2+$ TwoWay=�$ PropertyChanged=��� 5  .   $�� з  ��68   $
��Bold"�6D   $��Center=�6V   $��Center=�6s   &   Xceed.Wpf.Toolkit.DateTimePicker  5  .   $��Center=�6F   $��Center=�5  J   {�5  c   �� $ Check3_2��$/ 	IsChecked��  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]4   Value4 5  K   �� *StartHistoryDate+5  J   �� 5  .   $�� до  ��68   $
��Bold"�6E   $��Center=�6W   $��Center=�6t     5  .   $��Center=�6F   $��Center=�5  J   {�5  d   �� $ Check3_2��$/ 	IsChecked�4 5  J   �� *EndHistoryDate+5  J   �� 5  .   $��3q�6;   $��Center=�6F   .+�5  2   �� -   5        $ Check3_3��5  <   $!�� Піб оператора ��6N   $��Center=�6h   $
��Bold"�5  ;   $t nМожна щось одне: прізвище. ім'я чи по батькові. Більше шансів.��6M    5  ;   �� *Filter3.CheckArg3+$ PropertyChanged=��� 5  2   $	��5,5q�5  ;   $	��180��5   ;   $ 28��6G   $\ VМожно указать или имя или фамилию, или отчество��5!  '   ��5  ;   �� *Filter3.StrArg2+$ TwoWay=�{�5  H   �� $ Check3_3��$/ 	IsChecked�5!  '   5"  /   5%  +   �� 5'  *   $��Center=�65   $��Center=�6R   $��0,5,0,0q�6m   5+  +   �� 5.  *   $+��%Застосувати фільтри��61   $��Stretch=�6P   ���	#FF7E4A00$��3q�50  J   $��25��6U   $	��270��6d   ��5/  1   �� *CallFilter3Command+50  d   �� 51  *   $%��Очистити фільтри��61   $��3q�6L   $��25��6W   $	��270��6f   $��Stretch=�52  Y   
��� �����61   �� *ClearFilter3Command+53  1   54  '   55  #   56     5J     5K       5Q     $��1��6   $��3q�5S     ! .  $" Single=�5T  ;   # . $	��Top=�5U  4   $$ 30Q�5V     $��14(�6.   ��5R     �� *ReportOpsRecords+2 5W     �� 5X     "  6$   .�5Z  "   ��  � �� 5[     �� *
Released+#3 [ $ PropertyChanged=�5\     & 5_       5b     $' 0.40*
 61   $( Держ.номер ТЗ��6�   ) 6@   �� *VehicleNumber+$ PropertyChanged=�6�     5d     $' 0.34*
 61   $( Дата��6}   ) 6@   �� *	EventDate+$* {0:dd.MM.yy }��6}     5e     $' 0.34*
 61   $( Час��6}   ) 6@   �� *	EventDate+$* {0:hh.mm.ss }��6}     5f     $' 0.34*
 61   $( 
Подія��6�   ) 6@   �� *
Released+#3 [ 6�     5g     $' 0.84*
 61   $( ПІБ оператора��6^   ) 6@   �� *
UserData+6^   5m     5n     5o     5q     5r     5s     