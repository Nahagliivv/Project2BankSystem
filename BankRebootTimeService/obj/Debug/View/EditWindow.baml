   M S B A M L   `   `   `  ����P  LBankRebootTimeService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:BankRebootTimeService.ViewBankRebootTimeService.View  5   	   +    %BankRebootTimeService.View.EditWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:BankRebootTimeService.View     5   	     � Title$"  Редактирование��5   	   $��452.4��6    $	��405��6/    � 
ResizeMode$ NoResize=�6;   .��5	      � .+�5
   
   �� $��Имя:��6   $
��Left=�6   $��	10,49,0,0q�6:   $	��Top=�6M   �� 5   
   $��Фамилия:��6   $
��Left=�6#   $��	10,78,0,0q�6>   $	��Top=�6Q   �� 5   
   $��Должность:��6   $
��Left=�6%   $��
10,107,0,0q�6@   $	��Top=�6T   �� 5   
   $&�� Заработная плата:��6   $
��Left=�6,   $	��Top=�6G   $��
10,136,0,0q�6_   �� 5   
   $��Налог:��6   $
��Left=�6!   $	��Top=�6<   $��
10,165,0,0q�6T   �� 5   
   $,��&Необходимо оплатить:��6   $
��Left=�6/   $	��Top=�6J   $��
10,194,0,0q�6b   �� 5   
   $$��Забрал зарплату:��6   $
��Left=�6+   $	��Top=�6F   $��
10,223,0,0q�6^   �� 5   
   $7��1Кол-во раз неуплаты налога:��6   $
��Left=�66   $	��Top=�6Q   $��
10,252,0,0q�6i   �� 5   
   $��Накоплено ЗП:��6   $
��Left=�6(   $	��Top=�6C   $��
10,281,0,0q�6[   �� 5   
   $
��Left=�6D   $��23��6_   $��181,254,0,0q�6k    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��203��6�   ��6   �� *CountOfNotPaydTimes+ �� Mode$ TwoWay=�6�   �� 5   
   $
��Left=�67   $��23��6R   $��181,223,0,0q�6^   $	��Top=�6s   $	��203��6�    �� Text 6   �� *TakeS+$ TwoWay=�.@�5      �� True6   �� 5      False6   5      �� 5   
   $
��Left=�66   $��23��6Q   $��181,194,0,0q�6]   $
 Wrap=�6r   $	��Top=�6�   $	��203��6�   ��6   �� *ToPay+$ TwoWay=�6�   �� 5   
   $
��Left=�65   $��23��6P   $��181,166,0,0q�6\   $	��Top=�6r   $	��203��6�   ��6   �� *Tax+$ TwoWay=�6�   �� 5   
   $
��Left=�6:   $��23��6U   $��181,136,0,0q�6a   $
 Wrap=�6v   $	��Top=�6�   $	��203��6�   ��6   �� *	TheSalary+$ TwoWay=�6�   �� 5   
   $
��Left=�65   $��23��6P   $��181,107,0,0q�6\   $
 Wrap=�6q   $	��Top=�6�   $	��203��6�   ��6   �� *Post+$ TwoWay=�6�   �� 5   
   $
��Left=�6;   $��23��6V   $��
181,78,0,0q�6b   $
 Wrap=�6v   $	��Top=�6�   $	��203��6�   ��6   �� *
Secondname+$ TwoWay=�6�   �� 5   
   $
��Left=�6:   $��23��6U   $��
181,50,0,0q�6a   $
 Wrap=�6u   $	��Top=�6�   $	��203��6�   ��6   �� *	Firstname+$ TwoWay=�6�   �� 5   
   $
��Left=�68   $��23��6S   $��181,282,0,0q�6_   $
 Wrap=�6t   $	��Top=�6�   $	��203��6�   ��6   �� *ToTake+$ TwoWay=�6�   �� 5   
   $��Сохранить��61   $
��Left=�6E   $��185,381,0,0q�6`   $	��Top=�6u   $	��203��6�   ��6   �� *SaveChanges+6�   �� 5    
   $��N паспорта:��6   $
��Left=�6&   $��	10,20,0,0q�6A   $	��Top=�6T   �� 5!   
   $
��Left=�6;   $��23��6V   $��
181,20,0,0q�6b   $	��Top=�6v   $	��203��6�   ��6   �� *	PasNumber+$ TwoWay=�6�   �� 5"   
   $0��*Использовал фальшивки:��6   $
��Left=�61   $	��Top=�6L   $��
10,310,0,0q�6d   �� 5#   
   $
��Left=�6<   $��23��6W   $��181,311,0,0q�6c   $	��Top=�6x   $	��203��6�    6   �� *	UseAFalse+$ TwoWay=�.@�5$      �� True6   �� 5%      False6   5&      5(      5)      