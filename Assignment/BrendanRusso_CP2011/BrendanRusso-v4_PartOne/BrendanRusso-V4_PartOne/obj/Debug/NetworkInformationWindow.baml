   M S B A M L   `   `   `  ����  BrendanRusso-V3_PartOne6    0BrendanRusso_V3_PartOne.NetworkInformationWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  NetworkInformationWindow��5   	    � 
ResizeMode$ NoResize=�6*    � SizeToContent$ WidthAndHeight=�6@    � WindowStartupLocation$ CenterOwner=�6_    � WindowStyle$
 None=�6�   .��5      � $	��300��6   $	��120��6    � RowDefinitions 5   
   �� 5      �� 5      �� 5	      �� 5
      .+�5   
   �� $
��Left=�6   $��	10,13,0,0q�6/   $
��Wrap=�6B   $��IP Address:��6V   $	��Top=�6i   �� 5   
   -   5        $��lblPort��5   1   $��Center=�6   $��Center=�6@   $��port: 50000��6[   $��1��6n   �� 5   
   -   5        $��AddressField��5      $
��Left=�6&   $��23��6A   $��	73,10,0,0q�6M    �� TextWrapping$
 Wrap=�6`   $�� ��6t   $	��Top=�6|   $	��210��6�   6�   �� 5   
   -   5        $��	btnOkayIP��5      {�.  $��22��64   $��75��6@   $
��Okay��6K   $��3��6Z   �� 5   
   -   5        $��	slidePort��5      $��2��6"   $��Center=�6/    � Maximum$ 65535Q�6J    � Minimum$ 49152Q�6Z   	 � Value$	 50000Q�6j   5      5      