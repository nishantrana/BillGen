   M S B A M L   `   `   `  ����  BillGen'    !BillGen.CompanyInfo.ManageCompany   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Manage Company��5   	   $	��535��6    $
��1040��6-   .��5      � .+�5   
   �� -   5         ��Name$ spClientDetail��5   2   $��1��6   $
��1039��6%   ���	#FFF7631D J� OpacityMask �	#FFF70000$��0,0,0,0q�6y   $y�Visible=�6�   .+�5      �� $��COMPANY DETAIL��6   $��	0,0,0,-15q�6-   $	��Top=�6@   ���	#FFB20101
�������$
��Bold"�6�   $��17(�6�   $
��1040��6�   $��20,5,5,5q�6�   $��Center=�6�   �� 5      -   5         ��Name$ lstViewCompanyNames��5      $	��400��64   $��15(�6A   $��0,16,0,0q�6O    �� SelectionMode$ Single=�6a    �� View 5	      �� 5
      .P�5      �� $��Id��6)    �� Width$ 50��65    �� DisplayMemberBinding 6@   �� *cId+6g   �� 5      $
��Name��6)   $	 220��67    6C   �� *cName+6l   �� 5      $��GSTIN��6)   $	 200��68    6D   �� *cGSTIN+6n   �� 5      $��Address��6)   $	 280��6:    6F   �� *
cAddress+6r   5      5      �� 5      -   5         ��Name$ btnCompanyAdd��5      $��40��6,   $	��100��68   $��17(�6D   $��1��6R   	 � ToolTip$	 Add Company��6b   $��ADD NEW��6x   $
��Left=�6�   $��
605,15,0,0q�6�   6�   �� 5      -   5        $ btnCompanyUpdate��5      $��40��6/   $	��100��6;   $��17(�6G   $��1��6U   $	 Update Company��6e   $��UPDATE��6~   $
��Left=�6�   $��755,-40,0,0q�6�   6�   �� 5      -   5        $ btnCompanyDelete��5      $��40��6/   $	��100��6;   $��17(�6G   $��1��6U   $	 Delete Company��6e   $��DELETE��6~   $
��Left=�6�   $��910,-40,0,0q�6�   6�   5      5      5      