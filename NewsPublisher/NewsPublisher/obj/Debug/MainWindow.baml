   M S B A M L   `   `   `  ����H  DNewsPublisher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:NewsPublisherNewsPublisher  5   	       NewsPublisher.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:NewsPublisher     5   	     � Title$  
MainWindow��5   	   $
��1000��6   $
��1400��6*   .��5	      � $��0,0,2,4q�6    � ColumnDefinitions 5   
   �� 5      $	��33* �6   �� 5      $	��91* �6   .+�5   
   �� -   5        $��	comboBox1��5       �� 
IsReadOnly . .@�5      �� -   5        $
��cbi1��5      .��6(   Item16/   �� 5      -   5        $
��cbi2��5      .��6(   Item26/   �� 5      -   5        $
��cbi3��5      .��6(   Item36/   5      �� 5   
   -   5         ��Name$ 
urlTextBox��5      $
��Left=�6&   $��41��6A   $��
270,88,0,0q�6M    �� TextWrapping$
 Wrap=�6a   $	��Top=�6u   $	��748��6�   $��2��6�   �� 5   
   -   5         ��Name$ fetchButton��5      $��Fetch Images and Text��6&   $
��Left=�6F   $��	10,68,0,0q�6a   $	��Top=�6t   $	��151��6�    J� RenderTransformOrigin$ 3.212,0.916&�6�   6�   �� 5   
   -   5         ��Name$ image��5      $
��Left=�6   $	��212��6:   $��
209,56,0,0q�6G   $	��Top=�6[   $	��200��6s   $��2��6   �� 5   
   -   5        $ 
prevButton��5      $
��Prev��6%   $
��Left=�64   $��
86,130,0,0q�6O   $	��Top=�6c   $��75��6{   6�   �� 5   
   -	   5        $ 
nextButton��5      $
��Next��6%   $
��Left=�64   $��
86,177,0,0q�6O   $	��Top=�6c   $��75��6{   6�   �� 5   
   -
   5        $ headingTextBox��5      $
��Left=�6*   $��41��6E   $��
42,389,0,0q�6Q   $
 Wrap=�6e   $	��Top=�6y   $	��600��6�   $��2��6�   �� 5   
   -   5        $ textTextBox��5      $��2��6'   $
��Left=�6;   $	��135��6V   $��
42,456,0,0q�6c   $
 Wrap=�6w   $	��Top=�6�   $	��600��6�   " �� VerticalScrollBarVisibility$
 Auto=�6�   �� 5   
   -   5        $ publishButton��5      $��Publish��6(   $��1��6:   $
��Left=�6J   $��588,614,0,0q�6e   $	��Top=�6z   $	��109��6�   $��41��6�   6�   �� 5   
   -   5        	 ��Name$	 notificationTextBlock��5      $
��Left=�63   $��472,177,0,0q�6N   $
��Wrap=�6c   $	��Top=�6w   $	��145��6�   $	��225��6�   $��1��6�   �� 5   
   -   5        $ fetchImageButton��5      $��Fetch Image��6+   $��1��6A   $
��Left=�6Q   $��269,337,0,0q�6l   $	��Top=�6�   $��81��6�   6�   �� 5   
   -   5        $ altImageUrlTextBox��5      $
��Left=�6.   $��33��6I   $��
42,289,0,0q�6U   $
 Wrap=�6i   $�� ��6}   $	��Top=�6�   $	��600��6�   $��2��6�   �� 5    
   -   5        $ altNextText��5       $
��Next��6&   $��1��65   $
��Left=�6E   $��139,614,0,0q�6`   $	��Top=�6u   $��75��6�   6�   �� 5!   
   -   5        $ prevAltTextButton��5!      $
��Prev��6,   $
��Left=�6;   $��
86,614,0,0q�6V   $	��Top=�6j   $��75��6�   6�   �� 5"   
   -   5        
 ��Name$
 label��5"      $��Heading��6   $
��Left=�61   $��
42,358,0,0q�6L   $	��Top=�6`   �� 5#   
   -   5         ��Name$ comboBox��5#      $
��Left=�6%   $��	10,10,0,0q�6@   $	��Top=�6S   $	��120��6k   5%      5&      