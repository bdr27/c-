   M S B A M L   `   `   `  ����  ServerMiniCheckers-    'ServerMiniCheckers.Modal.IPAddressModal   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  	IPAddress��5   	    � 
ResizeMode$ NoResize=�6    � SizeToContent$ WidthAndHeight=�61    � WindowStartupLocation$ CenterOwner=�6P    � WindowStyle$
 None=�6t   .��5      � $	��300��6   $	��100��6    � RowDefinitions 5   
   �� 5      �� 5      �� 5	      .+�5   
   �� $��
IP Address��6   $��0��6&   $��20 0 0 0q�63   $��Center=�6E   �� 5   
   -   5         ��Name$ tbIPAddress��5      $��0��6'   $	��200��64   $��0 5 20 5q�6@   $��Right=�6R   6n   �� 5   
   $��Port Number��6   $��1��6'   $��20 0 0 0q�64   $��Center=�6F   �� 5   
   -   5         ��Name$ sldPortNumber��5      $��Right=�6(   $	��140��6D   $��0 5 20 5q�6P   $��1��6b   $��Center=�6o    � Minimum$ 49152Q�6�   	 � Maximum$	 65535Q�6�   
 � Value$
 50000Q�6�   �� 5   
   -   5         ��Name$ tbPortNumber��5      $��1��6*   $��	100 0 0 0q�67   $
��Left=�6J   $��Center=�6e   $��35��6�   ��6�   ��  �� ElementName$ sldPortNumber�� �� Path$ Value��� 5   
   -   5         ��Name$ btnOkay��5      $
��Okay��6"   $��Center=�61   $��2��6O   $��Center=�6\   $��75��6w   {�.  6�   �� 5   
   -   5        $ 
btnLazyAss��5      $
��Lazy��6%   $��Right=�64   $��2��6P   $��Center=�6]   $��75��6x   {�. 6�   5      5      