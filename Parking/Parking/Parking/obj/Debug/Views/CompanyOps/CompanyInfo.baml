   M S B A M L   `   `   `  ����B  >Parking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Parking.Views.CompanyOpsParking.Views.CompanyOps  5   	   K*clr-namespace:Parking.ViewModel.CompanyOpsParking.ViewModel.CompanyOps  5   	   *    $Parking.Views.CompanyOps.CompanyInfo   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:Parking.Views.CompanyOps     5   	   A
companyops*clr-namespace:Parking.ViewModel.CompanyOps     5   	     � WindowStyle$
  None=�5   
    � Title$ CompanyInfo��5
   	   $	��350��6   $	��300��6*    J� InputBindings 5      �� 5   
    �� Gesture$	 Esc��6    �� Command 6#   �� *CloseWindowCommand+ �� CommandParameter 5      ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" �.��5   
   � 	 � RowDefinitions	 5   
   �� 5      $��0.85* �6   �� 5      $��0.35* �6   �� 5      $��0.12* �6   .+�5   
   �� .+�5      �� $*��$Назва підприємства ��6   $
��Wrap=�63   $��16(�6G   $
��Bold"�6U   $��0,0,0,6q�5      $��Center=�6)   
����  �6F   �� 5      $	��280��6b   $��Center=�5      
 �� TextWrapping$

 Wrap=�69   $��0,0,0,6q�6M   $��14(�5       �� TextAlignment$ Center=�6&   $��Center=�6>   $
��Bold"�6[   ��5      �� *OwnerCompany.OrgName+ �� UpdateSourceTrigger$ PropertyChanged=�5   [   �� 5      $.��(Ідентифікаційний код ��6   $
��Wrap=�65   $��16(�6I   $
��Bold"�6W   $��0,0,0,6q�5      $��Center=�6)   
����  �6F   �� 5      $	��280��6b   $��Center=�5      $��0,0,0,6q�69    �� 	MaxLength$ 10��6J   $��14(�5      $ Center=�6&   $��Center=�6>   $
��Bold"�6[   ��5      �� *OwnerCompany.TaxCode+$ PropertyChanged=�5   [   �� 5       $,��&Реэстраційний номер ��6   $
��Wrap=�64   $��16(�6H   $
��Bold"�6V   $��0,0,0,6q�5!      $��Center=�6)   
����  �6F   �� 5"      $	��280��6d   $��Center=�5#      $��0,0,0,6q�69   $ 6��6J   $��14(�5$      $ Center=�6&   $��Center=�6>   $
��Bold"�6[   ��5"      �� *OwnerCompany.RegNumber+$ PropertyChanged=�5$   [   �� 5%      $A��;Адреса реэстрації підприємства ��6   $
��Wrap=�6?   $��16(�6S   $
��Bold"�6a   $��0,0,0,6q�5&      $��Center=�6)   
����  �6F   5'      �� 5)   
   $��1��6   $

 Wrap=�5*   d   $	��280��6x   $��Center=�5+      $��3q�69   $ 6��6D   $��14(�5,      $ Center=�6&   $��Center=�6>   $
��Bold"�6[   ��5*      �� *OwnerCompany.OrgAdress+$ PropertyChanged=�5,   [   � 5-   
   $��2��6    � ColumnDefinitions 5.      �� 5/      �� 50      .+�52      �� $�� Зачинити ��6   
���  �� � ToolTip$, &або гаряча клавіша Esc��54      ��52   <   �� *CloseWindowCommand+��53      ��  �� $ FindAncestor=�" �54      �� 55      $��1��6   $�� Зберегти ��6%   
��� � ���6M   �� *SaveDataCommand+56      57      58      