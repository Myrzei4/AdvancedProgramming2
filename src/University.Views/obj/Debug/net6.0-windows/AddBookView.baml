   M S B A M L   `   `   `  ����  University.Views"    University.Views.AddBookView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5	      �� 5
      $6 0/University.Views;component/SharedResources.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $
��Back��6   $��0��6    $��0��6-   $��5,5,5,0q�6V   $	��100��6g   $
��Left=�6s      PrimaryButton#��[  6�   
 � Tag$	 ��6�   ��6=   �� *Back+6�   �� 5   
   $
��Save��6   $��0��6    $��1��6-   $��5,5,5,0q�6V   $	��100��6g   $��Right=�6s   #��[  6�   $	 ��6�   ��6=   �� *Save+6�   �� 5   
   $��Personal Data��6   $��1��6*   $��0��67   $��5q�6G   $��10q�6R   .��5      �� .+�5      � $��0,0,0,5q�6   .+�5      �� $��Title��6      PrimaryTextBlock#��[ 6-   �� 5        PrimaryTextBox#��[ 6Q   ��6   �� *Title+ �� ValidatesOnDataErrors$
 True��6Q   5      � 5       $��0,0,0,5q�6   .+�5!      �� $��Author��6    #��[ 6.   �� 5"      #��[ 6R   ��6   �� *Author+$
 True��6R   5#      � 5$      $��0,0,0,5q�6   .+�5%      �� $��	Publisher��6    #��[ 61   �� 5&      #��[ 6U   ��6   �� *	Publisher+$
 True��6U   5'      � 5(      $��0,0,0,5q�6   .+�5)      �� $��Publication Date��6    #��[ 68   �� 5*      #��[ 6?   ��6   �� *PublicationDate+6?   5+      � 5,      $��0,0,0,5q�6   .+�5-      �� $
��ISBN��6    #��[ 6,   �� 5.      #��[ 6P   ��6   �� *Isbn+$
 True��6P   5/      � 50      $��0,0,0,5q�6   .+�51      �� $��Genre��6    #��[ 6-   �� 52      #��[ 6Q   ��6   �� *Genre+$
 True��6Q   53      � 54      $��0,0,0,5q�6   .+�55      �� $��Description��6    #��[ 63   �� 56      #��[ 6W   ��6   �� *Description+$
 True��6W   57      58      59      �� 5;   
   $��2��6.   $��2��6;   $��Center=�6O   $��0,0,0,5q�6l   ��6   �� *
Response+6l   5<      5=      