   M S B A M L   `   `   `  ����  University.Views#    University.Views.SubjectsView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5	      �� 5
      $6 0/University.Views;component/SharedResources.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ]� $��0��6   $��False��6!   .+�5      �� $��Add Subject��6   $��Right=�6C   $��5q�6Z      PrimaryButton#��[  6e   
 � Tag$	 ��6�   ��6+   �� *Add+6�   5      �� 5   
   $��1��6   $��0q�6A     ListViewPrimaryItem#��[ 6U   ��6    �� *
Subjects+ �� View 5      �� 5      .P�5      �� $
��Name��6%    �� DisplayMemberBinding 63   �� *Name+ �� CellTemplate 5      �� 5      .R�5   "   �� ��6,   �� *Name+5      5      �� 5       $��Semester��6%    67   �� *
Semester+ 5!      �� 5"      .R�5#   "   �� ��6,   �� *
Semester+5$      5&      �� 5'      $��Lecturer��6%    67   �� *
Lecturer+ 5(      �� 5)      .R�5*   "   �� ��6,   �� *
Lecturer+5+      5-      �� 5.      $��Students��6%    �� Width$	 200��67    5/      �� 50      .R�51   "   9� $
��View��6+   $��2q�69   .��52   &   �� ��63   �� *
Students+��53   *   �� 54   .   .R�55   2   � $��0,0,0,5q�67   .+�56   6   �� ��57   :   `� 58   >   	 �� StringFormat$	 	 {0} {1} ��6K   .1�59   B   �� 
 �� Path$

 Name�6J   �� 5:   B   $
 LastName�6J   5;   ?   5=   7   5>   3   5?   /   5A   '   5B   #   5C      5E      �� 5F      $
��Edit��6%    5G      �� 5H      .R�5I   "   ��   SecondaryButton#��[ 6�   $	 ��6�   ��6)   �� *DataContext.Edit+ �� RelativeSource �� *FindAncestor+ �� AncestorType UserControl��6�   �� *	SubjectId+6�   5J      5L      �� 5M      $��Remove��6%    5N      �� 5O      .R�5P   "   �� #��[ 6�   $	 ��6�   ��6)   �� *DataContext.Remove+ �� *FindAncestor+ UserControl��6�   �� *	SubjectId+6�   5Q      5S      5T      5V      5W      5X      