   M S B A M L   `   `   `  ����G  CPhoneBookWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:PhoneBookWpf.Views.WindowsPhoneBookWpf.Views.Windows  5   	   A%clr-namespace:PhoneBookWpf.ViewModelsPhoneBookWpf.ViewModels  5   
   /    )PhoneBookWpf.Views.Windows.DemoViewWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:PhoneBookWpf.Views.Windows     5   	   4vm%clr-namespace:PhoneBookWpf.ViewModels     5   
   -   5        $��MWindowDemo��5   	     � WindowStartupLocation$  CenterScreen=�5	   	    � Title$2 ,Демо версия справочника��5
   	   $	��450��5   	   $	��800��6    � DataContext 5      1  +PhoneBookWpf.ViewModels.DemoViewWindowModel  5   
   .��5      ]� .+�5   
   o� $	��Top=�6   .@�5      m� $��Файл��6   .@�5      m� $��
Выход��6    m� Command 5      �� *LoginCommand+ m� CommandParameter 5      ��  �� ElementName$ MWindowDemo��5      5      � 5   
   .+�5      �� -   5        $��AllContacts��5      $ �� IsSynchronizedWithCurrentItem$
 TrueY�5!      $��13(�5"      ��5      �� *
Contacts+ �� SelectedItem 5       �� *SelectedItem+ � 	Resources 5$      	 �� 
TargetType
 �� Property �� Value �� EndPoint �� 
StartPoint � Color � Offset��%  '��       �� 5%      "	 ��6   .�5&      �� 
 � �� 5'      �� 5(   "   $ 0.5,1&�66   $ 0.5,0&�6G   .��5)   &   � $ Black��63   $ 0Q�6A   � 5*   &   $ 	#FFCFF9F4��63   $ 1Q�6E   � 5+   &   $ White��63   $ 0Q�6A   5,   #   5.      5/      50       �� View 52      �� 53      .P�54      �� $��Фамилия��6)    �� DisplayMemberBinding 55   !   �� *
SecondName+�� 56      $��Имя��6)    57   !   �� *	FirstName+�� 58      $��Отчество��6)    59   !   �� *
MiddleName+5:      5<      5=      5>      5?      