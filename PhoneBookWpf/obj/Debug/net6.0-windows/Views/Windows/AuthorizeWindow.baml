   M S B A M L   `   `   `  ����G  CPhoneBookWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:PhoneBookWpf.ViewModelsPhoneBookWpf.ViewModels  5   	   0    *PhoneBookWpf.Views.Windows.AuthorizeWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4vm%clr-namespace:PhoneBookWpf.ViewModels     5   	   -   5        $��
AuthWindow��5
   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ Авторизация��5	   	   $	��250��5   	   $	��250��5   	   $	��350��5   	   $	��350��5   	    � DataContext 5      6  0PhoneBookWpf.ViewModels.AuthorizeWindowViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $	��45* �6   �� 5      $	��60* �6   �� 5      $	��70* �6   �� 5      $	��59* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5!   
   �� -   5         ��Name$ 	textLogin��5!      $��
20,10,20,0q�5"       �� TextWrapping$
 Wrap=�5#      $��30��5%      $��3��5&      $	��Top=�5'      $��14(�5(      $��5q�5)      $��1��6   ��5$      �� *Login+.��5)   '   ��5*      �� 5+       �� 
TargetType" ��6   .�5,      ��  �� Property � ��	 �� Value	 5-      �� 5.      
 �� 
TargetType"
 ��6.   .R�5/   "   �� #��z��6)   #��z��50   %   #��z��51   %   ._�52   &   � .+�53   *   ��  ��Name$ PlaceholderText��64   $"��Введите логин...��54   0   
�������$��14(�56   0   $��5q�57   0   $y�	Collapsed=�58   0    J� IsHitTestVisible$ False��59   0   �� 5:   *    ��Name$ PART_ContentHost��67   5;   '   5<   #    �� Triggers 5=   "   P� 5>   &    P� Property � �� P� Value$  ��6>   . �5?   *   ��  �� 
TargetName$ PlaceholderText��61    � y�$	 Visible=�6d   5@   '   5B      5D      5E      .��5F      5G      �� 5J   
   -   5        $ textPassword��5J      $��	20,5,20,0q�5K      $��30��5M      $��3��5N      $	��Top=�5O      $��14(�5P      $��5q�5Q      $��2��5R      ��5L      ��  �� Path$ Password�.��5R      ��5S      �� 5T      " ��6   .�5U      ��  � ��	 5V      �� 5W      "
 ��6.   .R�5X   "   �� #��z��6)   #��z��5Y   %   #��z��5Z   %   ._�5[   &   � .+�5\   *   �� $ PlaceholderText��64   $$��Введите пароль...��5]   0   
�������$��14(�5_   0   $��5q�5`   0   $y�	Collapsed=�5a   0   $ False��5b   0   �� 5c   *   $ PART_ContentHost��67   5d   '   5e   #    5f   "   P� 5g   &    � ��$  ��6>   . �5h   *   �� $ PlaceholderText��61    � y�$	 Visible=�6d   5i   '   5k      5m      5n      .��5o      5p      �� 5s   
   -   5         ��Name$ authorizationButton��5s      $��Вход��5t      $��Center=�5u      $��30��5x      $��90��5y      $��2��5z      $��3��5{      $��Center=�5|      $��14(�5}      ��5v      �� *LoginCommand+��5w      ��  �� ElementName$ 
AuthWindow��5}      �� 5   
   -   5        $ registerButton��5      $��Регистрация��5�      $��Center=�5�      $��90��5�      $��30��5�      $��Center=�5�      $��3��5�      $��14(�5�      ��5�      �� *RegisterCommand+��5�      �� $ 
AuthWindow��5�      �� 5�   
   -   5        $ 
skipButton��5�      $��Пропустить��5�      $��30��5�      $��Center=�5�      $��1��5�      $��14(�5�      $��3��6   $��	13,0,13,0q�6(   ��5�      �� *SkipCommand+��5�      �� $ 
AuthWindow��5�   (   �� 5�   
   -   5         ��Name$ error��5�      
���  ��$��Center=�5�      $��35��5�      $��12(�5�      $��3��6   $��	20,0,20,0q�6.   $
��Bold"�6A   ��5�      �� *ErrorContent+5�   A   5�      5�      