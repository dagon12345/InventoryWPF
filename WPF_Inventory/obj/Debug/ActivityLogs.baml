   M S B A M L   `   `   `  ����H  DWPF_Inventory, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WPF_InventoryWPF_Inventory  5   	        WPF_Inventory.ActivityLogs   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:WPF_Inventory     5   	   -   5          �Name$  	frm_login��5   ^    � Title$ ActivityLogs��6	   $	��700��6   $
��1000��6,    � WindowStartupLocation$ CenterScreen=�69   .��5	      � .+�5   
   �� $��Search by Date:��6   $��15,5,0,0q�6*   $
��Bold"�6<   $��2��6N   6d   �� 5   
   $��From:��6   $��	15,27,0,0q�6    $��2��63   6I   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��fromdatepicker��5      $��	20,50,0,0q�6+   $	��Top=�6>   $	��200��6V   $
��Left=�6b   6   �� 5   
   $	��To:��6   $��	15,80,0,0q�6   $��2��61   6G     5   
   -   5        $��todatepicker��5      $��
20,104,0,0q�6)   $	��Top=�6=   $	��200��6U   $
��Left=�6a   6~   �� 5   
   -   5        $��30��5      $	��200��6   $��
20,135,0,0q�6)   $��Search��6=   $
��Left=�6N   $	��Top=�6i    � Cursor$
 Hand��6�   6�   �� 5   
   -   5        $��btn_refresh��5   �   $��30��6   $	��200��6   $��
20,170,0,0q�6)   $��Refresh��6=   $
��Left=�6O   $	��Top=�6j   $
 Hand��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ datagrid��5      $��12��6%      
IsReadOnly .    SelectionUnit$ FullRow=�6F      AlternatingRowBackground
 ��ح� �� AlternationCount$ 2��5   
   $��
5,210,0,30q�6"   	   CanUserResizeRows	 .  $��2��6j   5      �� 5   
   $��25��6   $��Bottom=�6!   
���   �.@�5      �� -   5        $��	txtstatus��5      $��	User ID: ��6)   
�������$
��Bold"�6M   �� 5       -   5        $��
txtstatus1��5       $��
----------��6*   
�������$��Italic$�6O   �� 5!      $��2q�6   �� 5"      -	   5        $��txtusertypestatus��5"      $��
Usertype: ��61   
�������$
��Bold"�6V   �� 5#      -
   5        $��txtusertypestatus1��5#      $��
----------��62   
�������$��Italic$�6W   �� 5$      $��Connection Status: ��6   
�������$
��Bold"�6G   �� 5%      -   5        $��lblnetstatus��5%      $��
----------��6,   
�������$��Italic$�6Q   5(      5-      50      