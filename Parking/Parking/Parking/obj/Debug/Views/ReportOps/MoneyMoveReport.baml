   M S B A M L   `   `   `  ����B  >Parking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Parking.Views.ReportOpsParking.Views.ReportOps  5   	   /clr-namespace:Parking.HelpesParking.Helpes  5   	   /clr-namespace:Parking.HelpesParking.Helpes  5	   	   o<clr-namespace:Parking.ViewModel.ReportOps.MoneyMoveReportOps.Parking.ViewModel.ReportOps.MoneyMoveReportOps  5
   	   -    'Parking.Views.ReportOps.MoneyMoveReport   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Parking.Views.ReportOps     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   $wclr-namespace:Parking.Helpes  5   	   )helpesclr-namespace:Parking.Helpes  5	   	   Umoneymovereportops<clr-namespace:Parking.ViewModel.ReportOps.MoneyMoveReportOps  5
   	     � Title$a  [Звіт з руху грошових коштів, прийнятих персоналом��5   	   $	��450��6C   $
��1200��6P    � WindowStartupLocation$ CenterScreen=�5   	   .  (System.Windows.Interactivity.Interaction   	Behaviors 5      ,   &Parking.Helpes.HideCloseButtonOnWindow  5   
    � 	Resources 5      *   $Parking.Helpes.BoolToStringConverter   boolToStringConverter+   %Parking.Helpes.BoolToStringConverter2  boolToStringConverter22   ,Parking.Helpes.BoolToBrushConverterForStatus ! boolToBrushConverterForStatus1   +Parking.Helpes.BoolToBrushConverterForInOut   boolToBrushConverterForInOut  HeaderTemplate �� TextAlignment=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value�� 5   
   %x  &	        &	      &	 &     &	 9     &	 L     '  �     7    5      6+     5      6,     5      63     5      62   �� 5      .R�5      �� $
��Wrap=�6-   $ Center=�6B   ��6   �� 6B   5      �� 5      "  6   .�5      ��  � ��$ Center=�6?   �� 5        � ��$ 2q�6,   �� 5!       � ��8   64   5"      5#      .��5&      �� ���	#FF9BECE6$��3q�6&   ._�5'   
   �  � ColumnDefinitions 5(      �� 5)      $��0.64*  �6#   �� 5+      .+�5-      �� ���	#FF9BECE6$��2q�6.   ._�5.      � 	 � RowDefinitions	 5/      �� 50      $��0.06* �6(   �� 51      .+�53      �� $
��Left=�6"   
 �� Orientation$
 
Horizontal=�6=   .+�54      �� $/��)Загальна сума виручки ��6$   $
��Bold"�6B   
����  �$��Center=�55   (   $��Center=�6E   $��10,0,0,0q�6`   �� 56      $
��Bold"�6`   
����  �$��Center=�57   (   $��Center=�6E   ��56   $   �� *Total+ �� UpdateSourceTrigger$ PropertyChanged=�57   E   5:      &   System.Windows.Controls.DataGrid  5;      -   5          Name$ 
GridOrders��5;      $��1��63   $��3q�5>         AutoGenerateColumns .     SelectionMode$ Single=�5?   ;      
IsReadOnly . $	��Top=�5@   4      	RowHeight$ 30Q�5A      $��14(�6.   ��5<      �� *StuffMoneyRecords+ �� SelectedItem 5=      �� *SelectedStuffRec+   Columns 5B      0	  *System.Windows.Controls.DataGridTextColumn	  5C      ,
  &System.Windows.Controls.DataGridColumn 
  Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.72* 61    
  Header$ ПІБ оператора��6~   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   �� *PIB+$ PropertyChanged=�6~   	  5D      $ 0.32* 61   $! Співпрацюємо з��6�    6@   �� *StartDateWorking+ �� StringFormat$ {0:dd.MM.yy }��$ PropertyChanged=�6�   5F      5G      5H      �� 5M      ���	#FF9BECE6$��2q�6.   $��1��6B   ._�5N      � 	 5O      �� 5P      $��0.06* �6(   �� 5Q      �� 5R      $��0.06* �6(   .+�5T      �� $
 
Horizontal=�6!   .+�5U      �� $
 
Horizontal=�6%   $��Center=�6>   .+�5V      �� $$��Задати проміжок ��6(   $
��Bold"�6@   
����  �$��Center=�5W   (   $��Center=�6E   $��10,0,0,0q�6`   �� 5X      $
 
Horizontal=�6)   .+�5Y   "   �� $�� з  ��6,   $
��Bold"�68   $��Center=�6J   $��Center=�6g   (  "System.Windows.Controls.DatePicker  5Z   "      IsDropDownOpen .     SelectedDateFormat$ Short=�6D   $��Center=�5[   1   $��Center=�5]   1      SelectedDate 5\   1   �� *StartHistoryDate+$ PropertyChanged=� �� Mode$ TwoWay=�5]   1   �� 5^   "   $�� до  ��6,   $
��Bold"�69   $��Center=�6K   $��Center=�6h     5_   "    .  $ Short=�6D   $��Center=�5`   1   $��Center=�5b   1    5a   1   �� *EndHistoryDate+$ PropertyChanged=�$ TwoWay=�5b   1   �� 5c   "   $�� Показати ��6)   
����  ���6P   �� *CallFilterByDateCommand+5e      5f      5j        5l      $��1��6   $��3q�5n       .  $ Single=�5o   ;    . $	��Top=�5p   4   $ 30Q�5q      $��14(�6.   ��5m      �� *MoneyMoveRepDetailRecords+ 5s      	  5t      $ 0.40* 61   $ Держ.номер ТЗ��6_    6@   �� *	RegNumber+6_   	  5u      $ 0.34* 61   $ Сума��6[    6@   �� *Money+6[   	  5v      $ 0.34* 61   $* $Дата прийому оплати��6}    6@   �� *
PayingDate+$ {0:dd.MM.yy}��6}   	  5w      $ 0.34* 61   $ Подовжено до��6|    6@   �� *
DeadLine+$ {0:dd.MM.yy }��6|   5y      �� 5z      $��2��6!   $
 
Horizontal=�6.   .+�5{      �� $G��AСума виручки за обраним оператором ��6$   $
��Bold"�6O   
��� � �$��Center=�5|   (   $��Center=�6E   $��10,0,0,0q�6`   �� 5}      $
��Bold"�6c   
��� � �$��Center=�5~   (   $��Center=�6E   $��10,0,0,0q�6`   ��5}   $   �� *
SubTotal+$ PropertyChanged=�5~   `   5      5�      5�      5�      5�      5�      