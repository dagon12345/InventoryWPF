   M S B A M L   `   `   `  ����H  DWPF_Inventory, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WPF_InventoryWPF_Inventory  5   	       WPF_Inventory.frmUsers   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:WPF_Inventory     5   	     � Title$  Inventory Informations��5   	   $	��700��6(   $
��1000��65    � WindowStartupLocation$ CenterScreen=�6B   .��5	      � .+�5   
   �� $��Search Equipment Type��6   $
��Left=�60   $��10(�6K   $
��Bold"�6Y   $��15,1,0,0q�5      $	��Top=�6"   6<   �� 5   
   -   5         ��Name$ 	txtsearch��5      $
��Left=�6&   $��10(�6A   $��	15,30,0,0q�5      $	��Top=�6$   $��20��6<   $	��560��6H   6�   �� 5   
   -   5         ��Name$ 	btnsearch��5      $��Search��6$   $
��Left=�66    � Cursor$
 Hand��6Q   $��	15,56,0,0q�5      $	��Top=�6$   $��25��6<   $	��100��6H   6o   �� 5   
   -   5        $ 
btnrefresh��5      $��Refresh��6%   $
��Left=�68   $
 Hand��6S   $��
130,56,0,0q�5      $	��Top=�6%   $��25��6=   $	��100��6I   6q   �� 5   
   -   5        $��txtname��5      $��Name displayed: ��6#   $
��Bold"�6<   $��
15,100,0,0q�6N   �� 5   
   -   5        $��lblname��5      $��20��6"   $	��150��6.   $��
----------��6:   $
��Bold"�6M   $��Italic$�6_   $��115,100,0,0q�6r   $
��Left=�6�   $	��top=�6�    �� 
IsReadOnly . $��0q�6�   
���W�.�6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ datagrid��5         
IsReadOnly .    SelectionUnit$ FullRow=�69   	   AlternatingRowBackground
	 ��ح�
 �� AlternationCount$
 2��5   
   $��10,120,10,40q�6"      CanUserResizeRows .  $��2��6T   $��Stretch=�6h   �� 5   
   $��25��6   $��Bottom=�6    
���   �.@�5       �� -   5        $��	txtstatus��5       $��	User ID: ��6)   
�������$
��Bold"�6M   �� 5!      -   5        $��	lbluserid��5!      $��
----------��6)   
�������$��Italic$�6N   �� 5"      $��2q�6   �� 5#      -	   5        $��txtusertypestatus��5#      $��
Usertype: ��61   
�������$
��Bold"�6V   �� 5$      -
   5        $��lblusertype��5$      $��
----------��6+   
�������$��Italic$�6P   �� 5%      $��2q�6   �� 5&      $��Connection Status: ��6   
�������$
��Bold"�6G   �� 5'      -   5        $��lblnetstatus��5'      $��
----------��6,   
�������$��Italic$�6Q   5(      5+      5,      