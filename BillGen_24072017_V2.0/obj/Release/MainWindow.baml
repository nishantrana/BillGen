   M S B A M L   `   `   `  ����   BillGen    BillGen.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  BillGen��$	��677��$
��1150�� � UseLayoutRounding .   � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=� � WindowStyle$ SingleBorderWindow=�$y�Visible=����	#FFB20101 � Icon$/ )/BillGen;component/bin/Release/Logo/2.ico�� � ShowInTaskbar .  � 	Resources ��%    .��� $
��1140��$	��635��$��0,0,0,0q�.+�]� $��0��$	��100��$	��635��|�. ��Z� .+��� -    ��Name$ btnHome��$��40��$��86��$��0��   IconButtonStyle#���   $��15(�	 � ToolTip$
	 Home��$	��Top=�
 J� RenderTransformOrigin$
 0.5,0.5&�$��Center=�$��6,32,0,0q� �� 	IsDefault .  |�.  J� IsHitTestVisible . ��.  J� IsManipulationEnabled .  ���	#FF706A6A �� IsCancel .  {�. ����  �� ImageSource$' !/BillGen;component/Images/h91.jpg���� -   $ 
btnCompany��$��40��$��86��$��0��$��15(�$	 Add/Edit Company��$��Company��$��Center=�$
 0.5,0.5&�$
��Left=�$��-86,-50,0,0q� .  
���   �z�V� .~��� �� �� T�  T� Y$
 -195Q����� $&  /BillGen;component/Images/46.jpg���� -   $ btnBankDetail��$��40��$��86��$��0��$��15(�$	 Add/Edit Bank Details��$��Bank Detail��$��Center=�$
 0.5,0.5&�$
��Left=�$��
-86,40,0,0q�z�V� .~��� �� �� T� $
 -195Q����� $&  /BillGen;component/Images/46.jpg���� -   $ 
btnClients��$��40��$��86��$��15(�$��0��$	 Add/Edit Clients��$��Clients��$��Center=�$
 0.5,0.5&�$��
-86,70,0,0q�$
��Left=�$
��Left=�z�V� .~��� �� �� T� $
 -165Q����� $&  /BillGen;component/Images/46.jpg���� -   $ btnBill��$��40��$��86��$��15(�$��0��$	 Generate New/Modify Bill��$��New Bill��$��Center=�$
 0.5,0.5&�$
��Left=�$��-86,100,0,0q�z�V� .~��� �� �� T� $
 -135Q����� $&  /BillGen;component/Images/46.jpg���� -   $ btnViewBills��$��40��$��86��$��15(�$��0��$	 
View Bills��$��
View Bills��$��Center=�$
 0.5,0.5&�$
��Left=�$��-86,190,0,0q�z�V� .~��� �� �� T� $
 -135Q����� $&  /BillGen;component/Images/46.jpg���� -    ��Name$ spWelcomeScreen��$��1��$
��1039��$	��635�����	#FFF7631D J� OpacityMask �	#FFF70000$y�Visible=�$��Stretch=�$��0,0,0,0q�.+��� $#��WELCOME TO THE BILL GENERATOR��$��0,0,0,15q�$	��Top=����	#FFB20101
�������$
��Bold"�$��17(�$
��1040��$��20,5,5,5q�$��Center=��� $ ��A PRODUCT FOR  ANKIT IMPEX��$��	20,0,0,-4q�$
��25px(�$��Center=��� $��By WAPPZ��$��	20,0,0,-4q�$
��15px(�$��Center=��� -   $��Contact��$	��400��$	��170��$��0,45,0,0q����	#FFCB1515.+��� $��ABOUT��$��Center=�$��15(�$��30,5,0,0q��� $E��?BillGen is a bill generator and help to reduce the papaer work.��$��Center=�$��	40,30,0,0q��� $��CONTACT��$��Center=�$��15(�$��	30,65,0,0q��� $,��&Nikunj Nareda : nikunjnareda@gmail.com��$��Center=�$��	40,90,0,0q��� $/��)Nishant Rana   : nishant.rana25@gmail.com��$��Center=�$��
40,110,0,0q��� $�� For more info : ��$
��Bold"�$��15(�$��Center=�$��
40,140,0,0q��� -	   $��http://www.wappz.in?����	#FF212121.�www.wappz.in�� -
   $ 	spCompany��$��1��$
��1039�����	#FFF7631D �	#FFF70000$��0,0,0,0q�$y�	Collapsed=�$��Stretch=�.+��� $��COMPANY DETAIL��$��0,0,0,15q�$	��Top=����	#FFB20101
�������$
��Bold"�$��17(�$
��1040��$��20,5,5,5q�$��Center=��� $
��Name��$	��100��$��15(�$��
20,0,0,-10q�$
��Left=��� -    ��Name$ cbCompanyName��$��30��$��15(�$	��400��$��130,-20,0,0q�$
��Left=�  �� IsTextSearchCaseSensitive . �� $��TIN No.��$��15(�$	��100��$
��Left=�$��	20,10,0,0q��� -    ��Name$ txtBoxCompanyTIN��$��30��$	��400��$��15(� �� 
IsReadOnly .  $��1��$
��Left=�$��130,-30,0,0q��� $��CST No.��$��15(�$	��100��$��	20,10,0,0q�$
��Left=��� -   $ txtBoxCompanyCST��$��30��$	��400��$��15(� .  $��1��$
��Left=�$��130,-30,0,0q��� $��Address��$��15(�$	��100��$
��Left=�$��	20,10,0,0q��� -   $ txtBoxCompanyAddress��$��30��$	��400��$��15(�$��1��$
��Left=�$��130,-30,0,0q��� $��
Mobile No.��$��15(�$	��100��$
��Left=�$��	20,10,0,0q��� -   $ txtBoxCompanyMobile1��$��30��$	��400��$��15(�$��1��$
��Left=�$��130,-30,0,0q��� -   $ txtBoxCompanyMobile2��$��30��$	��400��$��15(�$��1��$
��Left=�$��	130,5,0,0q��� -   $ txtBoxCompanyMobile3��$��30��$	��400��$��15(�$��1��$��	130,5,0,0q�$
��Left=��� -   $ btnCompanyUpdate��$��40��$��75��$��1��$	 Update Company��$��Update��$
��Left=�$��17(�$��
455,23,0,0q��� -   $ spBankDetail��$��1��$
��1039�����	#FFF7631D �	#FFF70000$��0,0,0,0q�$y�	Collapsed=�.+��� $��BANK DETAIL��$��	0,0,0,-15q�$	��Top=����	#FFB20101
�������$
��Bold"�$��17(�$
��1040��$��20,5,5,5q�$��Center=��� $��Company��$	��100��$��15(�$��20,30,0,-10q�$
��Left=��� -   $ cbBankCompanyName��$��30��$��15(�$	��400��$��130,-20,0,0q�$
��Left=� . �� $��	Bank Name��$	��100��$��15(�$��20,10,0,-10q�$
��Left=��� -   $ txtBoxBankName��$��15(�$��30��$	��400��$��130,-20,0,0q�$
��Left=�
 � Tag$
 Name��$��1���� $��Address��$	��100��$��15(�$
��Left=�$��	20,15,0,0q�$��26���� -   $ txtBoxBankAddress��$��15(�$��30��$	��400��$��1��$
��Left=�$��130,-30,0,0q� . $��2���� $��A/c No.��$	��100��$��15(�$��	20,10,0,0q�$
��Left=��� -   $ txtBoxBankAccountNumber��$��15(�$��30��$	��400��$��1��$
��Left=�$��130,-30,0,0q�$��3���� $��	IFSC Code��$	��100��$��15(�$
��Left=�$��	20,10,0,0q��� -   $ txtBoxBankIfscCode��$��15(�$��30��$	��400��$��1��$
��Left=�$��130,-30,0,0q�$��4���� -   $ btnBankUpdate��$��40��$��75��$��17(�$��1��$	 Update Bank Detail��$��UPDATE��$
��Left=�$��
455,20,0,0q�$��5���� -   $ spClientDetail��$��1��$
��1039�����	#FFF7631D �	#FFF70000$��0,0,0,0q�$y�	Collapsed=�.+��� $��CLIENT DETAIL��$��	0,0,0,-15q�$	��Top=����	#FFB20101
�������$
��Bold"�$��17(�$
��1040��$��20,5,5,5q�$��Center=��� -    ��Name$ lstViewClientNames��$	��400��$��15(�$��0,16,0,0q� �� SelectionMode$ Single=� �� View �� .P��� $
��Name�� �� Width$	 220�� �� DisplayMemberBinding �� *cName+�� $��TIN/CST��$	 200�� �� *	cTINorCST+�� $��Address��$	 280�� �� *
cAddress+�� -   $ btnClientAdd��$��40��$	��100��$��17(�$��1��$	 
Add Client��$��ADD NEW��$
��Left=�$��
805,20,0,0q��� -   $ btnClientDelete��$��40��$	��100��$��17(�$��1��$	 Delete Client��$��DELETE��$
��Left=�$��910,-40,0,0q��� -   $ 	spNewBill��$��1��$
��1039��$	��635�����	#FFF7631D �	#FFF70000$��0,0,0,0q�$y�	Collapsed=�.+��� -    ��Name$ 
hdrNewBill��$��GENERATE NEW BILL��$��	0,0,0,-15q�$	��Top=����	#FFB20101
�������$
��Bold"�$��17(�$
��1040��$��20,5,9,5q�$��Center=��� $��Company��$��15(�$	��100��$��Disabled=�$��
20,25,0,-4q�$
��Left=��� -    $ txtBoxNBCompanyName��$
��Left=�$��Bottom=�$��30��$	��300��$��130,-25,0,0q� �� TextWrapping$ NoWrap=�$��1�� �� CharacterCasing$ Upper=� . �� $��Select Client��$��15(�$	��100��$��Disabled=�$��	20,5,0,-4q�$
��Left=��� -!   $ txtBoxNBClientName��$
��Left=�$��Bottom=�$��30��$	��300��$��130,-20,0,0q�$ NoWrap=�$��1��$ Upper=���   K� ZIndex$  10��.+��� -"   ! ��Name$! lbSuggestion��$  3��$��80��$ Single=����	#FF7D93CF$y�	Collapsed=�$
��Left=�$	��Top=�$	��300��$��	130,0,0,0q�$
 0.5,0.5&��� $��Bill Number��$��15(�$	��100��$��30��$��Disabled=�$��482,-30,0,-4q�$
��Left=��� -#   $ txtBoxNBBillNumber��$
��Left=�$��Bottom=�$��30��$	��300��$��590,-35,0,0q�$ NoWrap=�$��1��$ Upper=��� $  2��$��Delivery Date��$��15(�$	��100��$��30��$
��Left=�$��	20,10,0,0q�$��5q�(  "System.Windows.Controls.DatePicker  -$   "  Name$" dpNBDeliveryDate��$  2��$��15(�$��30��$	��300��$
��Left=�$��130,-30,0,0q�$��2���� $  2��$��	Bill Date��$��15(�$	��100��$��30��$
��Left=�$��482,-30,0,0q�$��5q�  -%   $" dpNBBillDate��$  2��$��15(�$��30��$	��300��$
��Left=�$��590,-35,0,0q�$��2���� $  2��$��
Sauda Date��$��15(�$	��100��$��30��$
��Left=�$��	20,10,0,0q�$��5q�  -&   $" dpNBSaudaDate��$  2��$��15(�$��30��$	��300��$
��Left=�$��130,-30,0,0q�$��2���� $  2��$��Due Date��$��15(�$	��100��$��30��$
��Left=�$��482,-30,0,0q�$��5q�  -'   $" dpNBDueDate��$  2��$��15(�$��30��$	��300��$
��Left=�$��590,-35,0,0q�$��2���� $��Broker��$  1��$��15(�$	��100��$��	20,10,0,0q�$
��Left=��� -(   $ txtBoxNBBroker��$��15(�$  1��$��30��$	��300��$��1��$
��Left=�$��130,-30,0,0q�$��3��$ Upper=��� $  10��.+��� -)   $! lblBrokerSuggestion��$  3��$��80��$	��300��$��	130,0,0,0q�$ Single=����	#FF7D93CF$y�	Collapsed=�$
��Left=�$	��Top=�$
 0.5,0.5&��� $��Add Particulars��$��15(�$	��110��$
��Left=�$��	20,10,0,0q��� $��Truck Number��$  1��$��13(�$	��100��$��140,-27,0,0q�$
��Left=��� -*   $ txtBoxNBTruckNumber��$��15(�$  1��$��30��$	��105��$��1��$
��Left=�$��	140,0,0,0q�$��3��$ Upper=��� $	��Qty��$��13(�$��50��$��30��$
��Left=�$��250,-85,0,0q��� -+   $ txtBoxNBQty��$��15(�$��30��$��50��$��1��$
��Left=�$��250,-30,0,0q�$��4���� $��	Qty Units��$��13(�$��85��$��30��$
��Left=�$��305,-85,0,0q��� -,   $ txtBoxNBQtyUnit��$
��Left=�$��Bottom=�$��30��$��85��$��305,-30,0,0q�$ NoWrap=�$��5��$ Upper=��� $  10��.+��� --   $! lblQUnitSuggestion��$  3��$��80��$ Single=����	#FF7D93CF$y�	Collapsed=�$
��Left=�$	��Top=�$��85��$��	305,0,0,0q�$
 0.5,0.5&��� $��Particulars��$��13(�$��30��$	��180��$
��Left=�$��395,-86,0,0q��� -.   $ txtBoxNBParticulars��$��15(�$��30��$	��180��$��1��$
��Left=�$��395,-30,0,0q�$��6��$ Upper=��� $  10��.+��� -/   $! lblParticularsSuggestion��$  3��$��80��$	��180��$��	395,0,0,0q�$ Single=����	#FF7D93CF$y�	Collapsed=�$
��Left=�$	��Top=�$
 0.5,0.5&��� $��Weight��$��13(�$��30��$��60��$
��Left=�$��580,-85,0,0q��� -0   $ txtBoxNBWeight��$��15(�$��30��$��60��$��1��$
��Left=�$��580,-30,0,0q�$��7���� $
��Unit��$��13(�$��30��$��60��$
��Left=�$��645,-85,0,0q��� -1   $ 	cbNBWUnit��$��15(�$
��Left=�$��Bottom=�$��30��$��55��$��645,-29,0,0q�{�.  # �� 
IsReadOnly# . $��8���� $
��Rate��$��13(�$��30��$��50��$
��Left=�$��705,-85,0,0q��� -2   $ txtBoxNBRate��$��15(�$��30��$��80��$��1��$
��Left=�$��705,-30,0,0q�$��9���� -3   $ btnNBAdd��$��15(�$��30��$��55��$��1��$	 	Add Entry��$	��Add��$
��Left=�$��794,-30,0,0q�$��10���� -4   $ btnNBModify��$��30��$��65��$��17(�$��1��$	 Modify Entry��$��Modify��$
��Left=�$��854,-30,0,0q�$��13���� -5   $ btnNBRemove��$��15(�$��30��$��65��$��1��$	 Remove Entry From Bill��$��Remove��$
��Left=�$��924,-30,0,0q�$��11���� -6   $ lstVwNewBill��$	��275��$	��850��$��15(�$��	90,10,0,0q� �� .P��� $��	Truck No.��$	 110�� �� *bpTruckNumber+�� $	��Qty��$ 55�� �� *
bpQuantity+�� $��Qty Unit��$ 90�� �� *bpQuantityUnit+�� $��Particulars��$	 185�� �� *bpParticulars+�� $��Weight��$ 65�� �� *
bpWeight+�� $��WUnit��$ 60�� �� *bpWeightUnit+�� $��	Rate / Qt��$ 90�� �� *bpRate+�� $��Amount��$	 113�� �� *
bpAmount+�� -7   $ btnNBGenerateNewBill��$��30��$	��100��$��17(�{�. $��1��$	 Generate New Bill��$��Generate��$
��Left=�$��	890,5,0,0q�$��12���� -8   $ btnNBGenerateModifyBill��$��30��$	��100��$��17(�{�.  $��1��$	 Modify Bill��$��Modify��$
��Left=�$��785,-30,0,0q�$��14���� -9   $ spChooseCompanyForBill��$��1��$
��1039��$	��635��$��0,0,0,0q�$y�	Collapsed=����	#FFF7631D.+��� $��CHOOSE COMPANY��$��	0,0,0,-15q�$	��Top=����	#FFB20101
�������$
��Bold"�$��17(�$
��1040��$��20,5,5,5q�$��Center=��� -:   $ btnCompany1��$��55��$	��400��$��1��$��ANKIT IMPEX CHENNAI��$
��Left=�$
��Bold"�$��25(�$��320,100,0,0q��� -;   $ btnCompany2��$��55��$	��400��$��1��$��ANKIT IMPEX INDIA��$
��Left=�$
��Bold"�$��25(�$��
320,15,0,0q��� -<   $ btnCompany3��$��55��$	��400��$��1��$��AGRO INDIA CORPORATION��$
��Left=�$
��Bold"�$��25(�$��
320,15,0,0q�$ � ColumnDefinitions$ �� $	��100 ��� $
��1045 �