   M S B A M L   `   `   `  ���� P  LBankRebootTimeService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:BankRebootTimeService.ViewBankRebootTimeService.View  +    %BankRebootTimeService.View.EditWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :local(clr-namespace:BankRebootTimeService.View       � Title$"  Редактирование��$��452.4��$	��405�� � 
ResizeMode$ NoResize=�.��� .+��� $��Имя:��$
��Left=�$��	10,49,0,0q�$	��Top=��� $��Фамилия:��$
��Left=�$��	10,78,0,0q�$	��Top=��� $��Должность:��$
��Left=�$��
10,107,0,0q�$	��Top=��� $&�� Заработная плата:��$
��Left=�$	��Top=�$��
10,136,0,0q��� $��Налог:��$
��Left=�$	��Top=�$��
10,165,0,0q��� $,��&Необходимо оплатить:��$
��Left=�$	��Top=�$��
10,194,0,0q��� $$��Забрал зарплату:��$
��Left=�$	��Top=�$��
10,223,0,0q��� $7��1Кол-во раз неуплаты налога:��$
��Left=�$	��Top=�$��
10,252,0,0q��� $��Накоплено ЗП:��$
��Left=�$	��Top=�$��
10,281,0,0q��� $
��Left=�$��23��$��181,254,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��203������ *CountOfNotPaydTimes+ �� Mode$ TwoWay=��� $
��Left=�$��23��$��181,223,0,0q�$	��Top=�$	��203�� �� Text �� *TakeS+$ TwoWay=�.@��� True�� False�� $
��Left=�$��23��$��181,194,0,0q�$
 Wrap=�$	��Top=�$	��203������ *ToPay+$ TwoWay=��� $
��Left=�$��23��$��181,166,0,0q�$	��Top=�$	��203������ *Tax+$ TwoWay=��� $
��Left=�$��23��$��181,136,0,0q�$
 Wrap=�$	��Top=�$	��203������ *	TheSalary+$ TwoWay=��� $
��Left=�$��23��$��181,107,0,0q�$
 Wrap=�$	��Top=�$	��203������ *Post+$ TwoWay=��� $
��Left=�$��23��$��
181,78,0,0q�$
 Wrap=�$	��Top=�$	��203������ *
Secondname+$ TwoWay=��� $
��Left=�$��23��$��
181,50,0,0q�$
 Wrap=�$	��Top=�$	��203������ *	Firstname+$ TwoWay=��� $
��Left=�$��23��$��181,282,0,0q�$
 Wrap=�$	��Top=�$	��203������ *ToTake+$ TwoWay=��� $��Сохранить��$
��Left=�$��185,381,0,0q�$	��Top=�$	��203������ *SaveChanges+�� $��N паспорта:��$
��Left=�$��	10,20,0,0q�$	��Top=��� $
��Left=�$��23��$��
181,20,0,0q�$	��Top=�$	��203������ *	PasNumber+$ TwoWay=��� $0��*Использовал фальшивки:��$
��Left=�$	��Top=�$��
10,310,0,0q��� $
��Left=�$��23��$��181,311,0,0q�$	��Top=�$	��203�� �� *	UseAFalse+$ TwoWay=�.@��� True�� False