   M S B A M L   `   `   `  ����G  CParcourFront, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ParcourFrontParcourFront  5   	       ParcourFront.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:ParcourFront     5   	     � Title$	  GPS��5   	   $	��450��6   $	��775��6"    � WindowStartupLocation$ Manual=�6.   .��5	      � $
��Auto��6   $
��Auto��6    � ColumnDefinitions 5
   
   �� 5      $��3* �6   �� 5      $��2* �6   .+�5   
   �� -   5        $��myTabControl��5      $��10q�6)   
�������$��3��6M    � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 ��Name
 �� Triggers P� Property P� Value �� 
TargetName��%J  '��       �� 5      " ��6   .�5      ��  � �� 5      �� 5      " ��6.   .R�5   "   �� $��Border��6)   $��1,1,1,0q�67   
�������$ 4,4,0,0��6i   $	��2,0q�6�   ._�5   &   �� $	 ContentSite��67   $��Center=�5   )   $��Center=�5   )   $��Header��5   )   $
��10,2q�5   )   5   #   
 5   "   P� 5   &    �@��
IsSelected . . �5   *   �� $ Border��61    � ��
 ��·�6[   5   '   P� 5   &    �@��
IsSelected .  . �5    *   �� $ Border��61    � ��
 �����6[   5!   '   5#      5%      5&      5'      .@�5(      �� $	��Map��6   .��5)      �  5*      �� 5+      $��3* �6+   �� 5,      $��2* �6+   .+�5.      �� -   5        $��CanvasCarte��5.      $��0��60   .+�5/      �� -   5        $��Carte��5/       $"��/ressources/carte-france.png��6-   $��3��6S   $
��Fill=�6  ��6�   �� *ActualWidth+ �� ElementName$ CanvasCarte�� �� Mode$ OneWay=���6�   �� *ActualHeight+$ CanvasCarte��$ OneWay=�6  50      �� 51      -   5        $��villes��51      $��1��6-   ��6=   �� *listeVillesAffichage+ �� View 52      �� 53      .P�54   "   �� $��Ville��61    �� Width$	 130��6@    �� DisplayMemberBinding 6L   �� *Name+�� 55   "   $��	Longitude��61   -   5        	Longitude��$ 80��56   8    57   1   �� *posX+�� 59   "   $��Latitude��61   -   5        Latitude��$ 80��5:   7    5;   1   �� *posY+5=      5?      5@      5A      �� 5B      $��Settings��6   .��5C      � .+�5D      �� -   5         ��Name$ Customer��5D   !   $��20q�63   $
��Auto��6?   $
��Auto��6L   .+�5E      �� -   5        $��NbrCheminQuestion��5E       $
��Left=�69   $	��Top=�6T   .��6l   <:Veuillez saisir le nombre de chemins à traiter en entrée6�   �� 5F      -	   5        $��NbrCheminAnswer��5F   "   $��	5,0,584,0q�69    � ForceCursor . 6_   5G      �� 5H      -
   5        $ 	Associate��5H   !   $��20,80,20,50q�64   $
��Auto��6I   $
��Auto��6V   .+�5I      �� -   5        $��NbrXoverQuestion��5I       $
��Left=�68   $	��Top=�6S   .��6k   <:Veuillez saisir le nombre de chemins traités par le Xover6�   �� 5J      -   5        $��NbrXoverAnswer��5J   "   $��	5,0,584,0q�68   6f   �� 5K      -   5        $	��Run��5K   !   $��5,10,0,0q�6,   $��72��6J   $
��Left=�6U   $	��Top=�6p   {�.  .��6�   Run6�   5L      5M      5N      �� 5O      $	��Run��6   .��5P      � $
��Auto��6   $
��Auto��6$    5Q      �� 5R      $��1* �6+   .+�5T      �� -   5        $��sortie��5T      $
��Auto��6-   $
��Auto��6:   $��2��6H    5U      �� 5V       �� AllowsColumnReorder .  �� ColumnHeaderToolTip$ Sortie��6B   .P�5W   "   �� $	 763��61   .��5X   &   �� .��6;   Statistiques d'exécution � ContextMenu 5Y   *   �� 5Z   .   -   5        $
��Stat��5Z   :   .@�5[   2   m� $��	Ascending��6;   m� 5\   2   $��
Descending��6;   5]   /   5_   '   5`   #   5a      5c      �� 5d      -   5        $��	resultats��5d      $
��Auto��60   $
��Auto��6=   $��	0,234,0,0q�6K   $��2��6^    5e      �� 5f       . $ Chemin List��6B   .P�5g   "   �� $	 763��61    6=   �� *Name+.��5h   &   �� .��6;   
Résultats 5i   *   �� 5j   .   -   5        $��Resultat��5j   :   .@�5k   2   m� $��	Ascending��6;   m� 5l   2   $��
Descending��6;   5m   /   5o   '   5p   #   5q      5s      �� 5t      -   5        $��showResultsOn��5t      $��	0,340,0,0q�62   $��72��6]   $
��Left=�6h   $	��Top=�6�   .��6�   Map6�   �� 5u      -   5        $��ResetRun��5u      $��	0,340,0,0q�6-   $��72��6N   $��Right=�6Y   $	��Top=�6u   $��1��6�   .��6�   Reset6�   5v      5w      5x      5y      5z      