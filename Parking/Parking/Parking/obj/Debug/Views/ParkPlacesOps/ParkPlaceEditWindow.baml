   M S B A M L   `   `   `  ����B  >Parking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Parking.Views.ParkPlacesOpsParking.Views.ParkPlacesOps  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   /clr-namespace:Parking.HelpesParking.Helpes  5   	   /clr-namespace:Parking.HelpesParking.Helpes  5
   	   Q-clr-namespace:Parking.ViewModel.ParkPlacesOpsParking.ViewModel.ParkPlacesOps  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel 6M   5    /Parking.Views.ParkPlacesOps.ParkPlaceEditWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:Parking.Views.ParkPlacesOps     5   	   9System&clr-namespace:System;assembly=mscorlib     5   	   /helpesclr-namespace:Parking.Helpes     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5	   	   $wclr-namespace:Parking.Helpes  5
   	   Aparkplacesops-clr-namespace:Parking.ViewModel.ParkPlacesOps  5   	   Cscm18clr-namespace:System.ComponentModel;assembly=WindowsBase  6M     � Title$  ParkPlaceEditWindow��5   	   $	��590��6%   $
��1200��62   .  (System.Windows.Interactivity.Interaction   	Behaviors 5      ,   &Parking.Helpes.HideCloseButtonOnWindow  5   
    � 	Resources 5      '   !Parking.Helpes.ImagePathConverter   imagePathConverter*   $Parking.Helpes.BoolToStringConverter  boolToStringConverter+   %Parking.Helpes.BoolToStringConverter2  boolToStringConverter2  HeaderTemplate �� TextAlignment=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value Win‌​dowsBaseX>clr-namespace:System.ComponentModel;assembly=Win‌​dowsBaseSystem.ComponentModel 5%        SortedItems �� Source �� SortDescriptions+  %System.ComponentModel.SortDescription	   PropertyNameX>clr-namespace:System.ComponentModel;assembly=Win‌​dowsBaseSystem.ComponentModel 5+        SortedTypes�� 5   
   %�  &	        &	      &	 &     &	 9     '  �     7  &	 %    &	 �      5      6(     5      6+     5      6,   �� 5      .R�5      �� $
��Wrap=�6-   $ Center=�6B   ��6   �� 6B   5      �� 5      "  6   .�5      ��  � ��$ Center=�6?   �� 5        � ��$ 2q�6,   �� 5!       � ��8   64   5"      �� 5$      Hscm>clr-namespace:System.ComponentModel;assembly=Win‌​dowsBase  5%       5$   7   �� *VehicleColors+ 5&        5'      $	 	ColorName��6+   5)      �� 5*      Fscm>clr-namespace:System.ComponentModel;assembly=Win‌​dowsBase  5+       5*   7   �� *VehicleTypeList+ 5,        5-      $	 TypeName��6+   5/      52      
 J� InputBindings
 55      �� 56   
    �� Gesture$ Alt+F4��6    �� Command 6&   �� *CencelWindowCommand+ �� CommandParameter 57      ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" �.��59      �� 
���2͚�$��4q�6'   ._�5:   
   � $��4q�6    � ColumnDefinitions 5;      �� 5<      $��0.33* �6#   �� 5?      �� 5@      $
��0.6* �6$   .+�5C      �� $��2��6   
���2͚�$��2q�6@   ._�5D      �  � RowDefinitions 5E      �� 5F      �� 5G      $
��0.1* �6(   .+�5I      &   System.Windows.Controls.DataGrid  -   5          Name$ 
GridOrders��5I      $��3q�5J         AutoGenerateColumns .     SelectionMode$ Single=�5K   /      
IsReadOnly . $	��Top=�5L   (      	RowHeight$ 30Q�5M      $��14(�6"   ��5I   3   �� *ParkPlaceHisrtoryRecords+   Columns 5O      0	  *System.Windows.Controls.DataGridTextColumn	  5P      ,
  &System.Windows.Controls.DataGridColumn 
  Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.32* 61    
  Header$ № місця��6^   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   �� *
PPNumber+6^   	  5Q      $
 0.5* 61   $ Дата��6^    6>   �� *DateOfEvent+6^   	  5R      $
 0.5* 61   $ Час��6^    6>   �� *TimeOfEvent+6^   	  5S      $
 0.5* 61   $  Заїхав/ виїхав��6�    6>   �� *
Released+ �� 	Converter# [ 6�   5U      �� 5V      $��1��6!   $��Center=�6.   .+�5W      �� $$��Задати проміжок ��6$   $
��Bold"�6<   $��Center=�6N   $��0,0,0,6q�6k   �� 5X       �� Orientation$ 
Horizontal=�6%   .+�5Y      �� $�� з  ��6(   $
��Bold"�64   $��Center=�6F   $��Center=�6c   (  "System.Windows.Controls.DatePicker  5Z      -   5          Name$ 	StartDate��5Z   )      IsDropDownOpen .      SelectedDateFormat$  Short=�6S   $��Center=�5[   1   $��Center=�5]   1   !   SelectedDate! 5\   1   �� *StartHistoryDate+" �� UpdateSourceTrigger$" PropertyChanged=�# �� Mode$# TwoWay=�5]   1   �� 5^      $�� до  ��6(   $
��Bold"�65   $��Center=�6G   $��Center=�6d     5_       .  $  Short=�6@   $��Center=�5`   1   $��Center=�5c   1   $   DisplayDateStart$ 5a   1   �� *StartHistoryDate+$" PropertyChanged=�! 5b   1   �� *EndHistoryDate+$" PropertyChanged=�$# TwoWay=�5c   1   5e      5h      5j      5k      � 5n      $��Stretch=�6    5o      �� 5p      $
��0.2* �6$   �� 5q      $
��1.2* �6$   �� 5r      $
��Auto �6$   .+�5v      �� $��Center=�6   .+�5w      �� $ 
Horizontal=�6!   .+�5x      �� $,��&Паркувальне місце № ��6%   $��12(�6A   $
��Bold"�6O   $��Center=�6a   
���  ��$
��Wrap=�6�   �� 5y      $��20(�6�   $
��Bold"�6�   $��Center=�6�   
����  ���6%   �� *0.CurrentRecord.SomeParkingPlace.ParkPlaceNumber+$" PropertyChanged=�6�   5z      �� 5{      $ 
Horizontal=�6!   $��0,6,0,0q�6:   .+�5|      �� $��Статус: ��6%   $��12(�65   $
��Bold"�6C   $��Center=�6U   	� 5}      % 	� 	GroupName$% D��6&   $��Вільне ��64   $��Center=�6F   & d� 	IsChecked& 6a   �� *+)CurrentRecord.SomeParkingPlace.FreeStatus+$" PropertyChanged=�	� 5~      $% D��6&   $��Зайняте��64   $��Center=�6F   & 6a   �� *	NotFree+$" PropertyChanged=�5      �� 5�      $ 
Horizontal=�6!   $��0,8,0,0q�6;   .+�5�      	� $% С��6&   $��Заїхав��64   $
��Bold"�6E   
����  �$��Center=�6j   & 5�   &   �� *)'CurrentRecord.SomeParkingPlace.Released+$" PropertyChanged=�	� 5�      $% С��6&   $��Виїхав��64   $
��Bold"�6F   
����  �$��Center=�6j   & 5�   &   �� *
NotInPlace+$" PropertyChanged=�5�      5�      � 5�      $��1��6    5�      �� 5�      $
��1.8* �6(   �� 5�      .+�5�      �� $��3q�6w   $��20(�5�   +   ' �� TextAlignment$' Center=�6:   ( �� TextWrapping$
( Wrap=�6Q   $��Stretch=�6e   $
��Bold"�6�   $��1��5�   +   ��5�      �� *#!CurrentRecord.SomeContacts.Adress+$" PropertyChanged=�5�   +   �� 5�      ���	#FFE9F7E9$��1q�64   
�������._�5�      �� .+�5�      �� $��1q�6%   
�������._�5�   "   �� $��Center=�6-   .+�5�   &   �� $ 
Horizontal=�61   .+�5�   *   �� -   5        ) ��Name$) OrgNameEnable��5�   3   $��Center=�6J   $��  ��6e   �� 5�   *   $��Організація ��65   $��14(�6I   $
��Bold"�6W   $��Center=�6i   ���	#FFBD00006�   5�   '   �� 5�   &   $��1��6.   $	��185��5�   �   $��14(�6�   * �� 	MaxLength$* 26��6�   $
��Left=�6�   $��3q�6�   $��Center=�6�   {�5�   >   �� + �� ElementName$+ OrgNameEnable��, �� Path$, 	IsChecked���5�   .   �� *" CurrentRecord.SomeClient.OrgName+$" PropertyChanged=�6�   5�   #   5�      �� 5�      $��Center=�6)   .+�5�   "   �� $��Водій ��6-   $��14(�6;   $
��Bold"�6I   $��Center=�6[   
���**��6x   �� 5�   "   $��Прізвище: ��6-   $��12(�6?   $
��Bold"�6M   $��Center=�6_   �� 5�   "   $��1��6*   $	��185��6�   $��14(�6�   $* 26��6�   $
��Left=�6�   $��3q�6�   $��Center=�6�   ��6:   �� *%#CurrentRecord.SomePerson.SecondName+$" PropertyChanged=�6�   �� 5�   "   $��	Ім'я: ��6-   $��12(�6;   $
��Bold"�6I   $��1��6[   $��Center=�6h   �� 5�   "   $	��185��6�   $��14(�6�   $* 26��6�   $
��Left=�6�   $��3q�6�   $��Center=�6�   ��6+   �� *$"CurrentRecord.SomePerson.FirstName+$" PropertyChanged=�6�   �� 5�   "   $��По батькові: ��6-   $��12(�6B   $
��Bold"�6P   $��2��6b   $��Center=�6o   �� 5�   "   $	��185��6�   $��14(�6�   $* 26��6�   $
��Left=�6�   $��3q�6�   $��Center=�6�   ��6+   �� *%#CurrentRecord.SomePerson.Patronimic+$" PropertyChanged=�6�   �� 5�   "   $ 
Horizontal=�6-   .+�5�   &   �� $��Стать: ��61   $��12(�6@   $
��Bold"�6N   $��Center=�6`   	� 5�   &   $% B��62   $��Жіноча ��6@   $��Center=�6R   & 6m   �� *CurrentRecord.FemaleOwnPers+$" PropertyChanged=�	� 5�   &   $% B��62   $�� Чоловіча��6@   $��Center=�6T   & 6o   �� *CurrentRecord.SomePerson.Sex+$" PropertyChanged=�5�   #   �� 5�   "   $��№ телефона: ��6-   $��12(�6A   $
��Bold"�6O   $��4��6a   $��Center=�6n   �� 5�   "   $	��185��6n   $��14(�6}   $* 13��6�   $
��Left=�6�   $��3q�6�   $��Center=�6�   ��6,   �� *OwnerPhone1+$" PropertyChanged=�6�   �� 5�   "   $��Адреса ��6,   $��14(�6;   $
��Bold"�6I   $��Center=�6\   5�      5�      5�      5�      5�      � 5�      $��1��6    5�      �� 5�      $
��0.9* �6$   �� 5�       5�      �� 5�      $
��1.2* �6'   �� 5�      $
��1.3* �6'   �� 5�      .+�5�      �� $��1��6   
���   �$��1q�6:   ���	#FFF2F4E3._�5�      �� $��Center=�6%   .+�5�      �� $"��Довірена особа ��6)   $��14(�6@   $
��Bold"�6N   $��Center=�6`   ���	#FFBD00006}   �� 5�      $��Прізвище: ��6)   $��12(�6;   $
��Bold"�6I   $��Center=�6[   �� 5�      $��1��6&   $	��170��6�   $��14(�6�   $* 26��6�   $
��Left=�6�   $��3q�6�   $��Center=�6�   ��66   �� *(&CurrentRecord.TrustedPerson.SecondName+$" PropertyChanged=�6�   �� 5�      $��	Ім'я: ��6)   $��12(�67   $
��Bold"�6E   $��1��6W   $��Center=�6d   �� 5�      $	��185��6�   $��14(�6�   $* 26��6�   $
��Left=�6�   $��3q�6�   $��Center=�6�   ��6'   �� *'%CurrentRecord.TrustedPerson.FirstName+$" PropertyChanged=�6�   �� 5�      $��По батькові: ��6)   $��12(�6>   $
��Bold"�6L   $��2��6^   $��Center=�6k   �� 5�      $	��185��6�   $��14(�6�   $* 26��6�   $
��Left=�6�   $��3q�6�   $��Center=�6�   ��6'   �� *(&CurrentRecord.TrustedPerson.Patronimic+$" PropertyChanged=�6�   �� 5�      $��Стать: ��6)   $��12(�68   $
��Bold"�6F   $��Center=�6X   $��Center=�6s   
���  ��6�   �� 5�      $ 
Horizontal=�6)   .+�5�   "   	� $% A��6.   $��Жіноча ��6<   $��Center=�6N   & 6i   �� *CurrentRecord.FemaleTrustPers+$" PropertyChanged=�	� 5�   "   $% A��6.   $�� Чоловіча��6<   $��Center=�6P   & 6l   �� *!CurrentRecord.TrustedPerson.Sex+$" PropertyChanged=�5�      �� 5�      $��№ телефона: ��6)   $��12(�6=   $
��Bold"�6K   $��4��6]   $��Center=�6j   �� 5�      $	��165��6i   $��14(�6x   $* 13��6�   $
��Left=�6�   $��1��6�   $��4��6�   $��3q�6�   $��Center=�6�   ��6'   �� *
TrustPhone+$" PropertyChanged=�6�   5�      5�      � 5�      $��3��6   $��4q�6+   .+�5�      �� $��Center=�6�   ��6   �� $&,  CurrentRecord.SomeVehicle.VPhoto�# [  $" PropertyChanged=�6�   - � ContextMenu- 5�      �� 5�      .@�5�      m� $ ��Видалити фото?��6'   . m� Command. 6?   �� *DeleteVehiclePhotoCommand+m� 5�      $+��%Додати/змінити фото?��6'   . 6E   �� *OpenFileDialogCommand+5�      ��5�      �� 5�      / �� ImageSource/ 6%   �� *DefaultPhoto+$" PropertyChanged=�5�      � 5�      $��1��6   $��1��6$    5�      �� 5�      �� 5�      $��0.75* �6(   .+�5�      �� $��3q�6   ._�5�      �� ���	#FFDEE8A1
�������$��1q�6K   ._�5�   "   �� $��	0,0,0,-10q�6-   .+�5�   &   �� $,��&Реєстраційний номер ��60   $
��Wrap=�6L   $��16(�6`   $
��Bold"�6n   $��Center=�6�   
���  ��6�   �� 5�   &   $	��185��6o   $��3q�6   $��24(�5�   '   $' Center=�66   $��Center=�6N   $
��Bold"�6k   0 � ToolTip$/0 )Лише латиниця та цифри��5�   '   ��5�   /   �� *	RegNumber+$" PropertyChanged=�5�   '   �� 5�   &   $��Колір ��60   $��14(�6>   $
��Bold"�6L   $��Center=�6_   
���  ��6|   �� 5�   "   -   5        1 ��Name$1 
ColorCombo��5�   +   $	��185��6@   $��Center=�6M   $��0,0,0,20q�6h   2 �� 
IsEditable2 . $3 �� IsSynchronizedWithCurrentItem$
3 TrueY�5�   <   4 �� SelectedValuePath$4 	ColorName��5�   -   $
��Left=�5�   *   5 �� DisplayMemberPath$5 	ColorName��5�   *   
�������$��14(�5�   *   ��5�   *   �� 6 �� Source#6 [ 7 �� SelectedValue7 5�   *   �� *SelectedColor.ColorName+8 �� SelectedItem8 5�   *   �� *SelectedColor+$" PropertyChanged=�5�   #   �� 5�   "   $ 
Horizontal=�6-   $��Center=�6F   $��Center=�6c   .+�5�   *   �� $��Тип ТЗ ��64   $
��Wrap=�6C   $��12(�6W   $
��Bold"�6e   $��Center=�5   0   $��Center=�6M   �� 5
  &   $	��130��61   $��Center=�6A   2 . $
3 TrueY�5  <   $4 TypeName��5  -   $
��Left=�5  *   $5 TypeName��5  *   
�������$��14(�5  *   ��5  *   �� #6 [ 7 5  *   �� *VType.TypeName+8 5  *   �� *VType+$" PropertyChanged=�9 �� Text9 5  1   �� *
NewVType+$" PropertyChanged=�5  '   5  #   5  #   5     5     �� 5     $��1��6   
�������$��1q�6=   ���	#FFBFDFE6$��-1q�6h   ._�5     �� ���	#FFDAEDED$��3q�6<   .+�5     �� $=��7Дата закінчення строку оренди��6(   $��12(�6M   $
��Wrap=�6[   $ Center=�6o   
���  ��$
��Bold"�6�   $��Center=�6�   �� 5      $��14(�6�   $ Center=�6�   
����  �$
��Bold"�6�   $��Center=�6�   ��6(   �� *NextDeadLine+: �� StringFormat$: {0:dd.MM.yy }��$" PropertyChanged=�6�   �� 5"     .+�5#  "   �� $3��-Подовжити дату оренди до��6,   $
��Bold"�6L   $��Center=�6^   $��0,0,0,1q�6{     5$  "    .  $  Short=�6E   $��Center=�5%  1   $��Center=�5(  1   ! 5'  1   �� *ProlongDate+$" PropertyChanged=�$# TwoWay=�5(  1   �� 5+  "   $
��Bold"�6r   $��Center=�6�   
����  ���6,   �� *ProlonDaysCount+$" PropertyChanged=�6�   5,     �� 5-     $ 
Horizontal=�6)   $��Center=�6B   $��Center=�6_   .+�5.  "   �� $ ��Внести оплату ��6,   $��14(�6B   $
��Wrap=�6P   $ Center=�6d   
���  ��$
��Bold"�6�   $��Center=�5/  4   $��Center=�6Q   �� 50  "   $��60��6g   $��14(�6u   $* 5��6�   $��Center=�6�   $��3q�6�   $��Center=�6�   ��6+   �� *Coast+$" PropertyChanged=�6�   51     52     53     55     �� 56     $��1��6   $��2��6*   .+�57     �� $��1q�6   ���	#FFCCE6BF
�������._�58     �� $��Center=�6%   .+�59     �� .+�5:  "   �� $C��=Підібрати інше паркувальне місце��6,   $
��Wrap=�6T   $ Center=�6h   
��� d �$
��Bold"�6�   $��Center=�6�   $��12(�6�   �� 5<  "   $ 
Horizontal=�6-   .+�5=  &   �� -   5        $) ReplaceEnable��5=  /   $��Center=�6F   $��  ��6a   {�5>  /   �� *NewDataAddedSaved+$" PropertyChanged=��� 5?  &   $��80��61   2 .  $
3 TrueY�5A  9   $��Center=�5E  &   
�������$��14(�5G  &   {�5@  &   �� $+ ReplaceEnable��$, 	IsChecked�7 5B  &   �� *FreeparkPlace+$" PropertyChanged=�8 5C  &   �� *FreeparkPlace+$# TwoWay=�$" PropertyChanged=���5D  &   �� *FreeParkingPlacesList+$" PropertyChanged=�0 5G  4   �� *MessageForChangeParkPlace+$" PropertyChanged=�5H  '   5I  #   �� 5J  "   $��Застосувати��5L  )   
��� � �$��25��6R   {�5J  )   �� $+ ReplaceEnable��$, 	IsChecked���5K  )   �� *ChangeParkinPlaceCommand+5L  R   5M     5N     5O     �� 5P     $��1q�6   
�������._�5Q     �� $��Center=�6%   .+�5T     �� .+�5U  "   �� $
��Wrap=�6u   $ Center=�6�   
���  ��$
��Bold"�6�   $��Center=�6�   $��12(�6�   ��6,   �� *LastPayDateMessage+$" PropertyChanged=�6�   �� 5Y  "   $#�� Друк квитанції ��6*   
���**��$��25��6X   {�6d   �� *NewDataAddedSaved+$" PropertyChanged=���5Z  *   �� *PrintQuitanceCommand+�� 5[  "   $��Зачинити��6*   $��25��6>   ��6J   �� *CencelWindowCommand+��5\  &   ��  �� $ FindAncestor=�" ��� 5]  "   $��Зберегти��6*   
����  �$��25��6O   ��6[   �� *SavedataCommand+��5^  *   �� $+ 
ColorCombo��$
, Text�5_     5`     5a     5b     5d     5p     5q     5r     