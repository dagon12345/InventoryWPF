   M S B A M L   `   `   `  ����H  DWPF_Inventory, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WPF_InventoryWPF_Inventory  5   	       WPF_Inventory.MainMenu   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:WPF_Inventory     5   	   -   5          �Name$  frm_mainmenu��5   5    � Title$ MainMenu��6	   $	��700��6   $
��1000��6'    � WindowStartupLocation$ CenterScreen=�6�   .��5
      �  � RowDefinitions 5   
   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��25��6   $��Bottom=�6    
���   �$��2��6N   .@�5      �� -   5        $��	txtstatus��5      $��	User ID: ��6)   
�������$
��Bold"�6M   �� 5      -   5        $��
txtstatus1��5      $��
----------��6*   
�������$��Italic$�6O   6b   �� 5      $��2q�6   �� 5      -   5        $��txtusertypestatus��5      $��
Usertype: ��61   
�������$
��Bold"�6V   �� 5      -   5        $��txtusertypestatus1��5      $��
----------��62   
�������$��Italic$�6W   �� 5      $��Connection Status: ��6   
�������$
��Bold"�6G   �� 5      -   5        $��lblnetstatus��5      $��
----------��6,   
�������$��Italic$�6Q   5      ]� 5   
   $��2��6   .+�5      o� -   5        $��panel��5   L   $��20��6   $	��Top=�6   $	��Top=�64   .@�5!      m� -   5        $��m_info��5!   ,   $��Options��6   .@�5#      m� -	   5        $��information��5#   >   $��Inventory Information��6    m� Icon 5$      �� 5%      $��Info.png��6$   5'      m� 5*      -
   5        $��	inventory��5*   2   $��	Inventory��6    5+      �� 5,      $��Box.png��6$   5.      m� 52      -   5        $��activitylogs��52   6   $��Activity Logs��6    54      �� 55      $��Logs.png��6$   58      m� 5;      -   5        $��registration��5;   5   $��Registration��6    5<      �� 5=      $��Register.png��6$   5?      m� 5C      -   5        $��logout��5C   0   $��Logout��6    5D      �� 5E      $��
Logout.png��6$   5G      5I      5L      5M      5P      5Q      