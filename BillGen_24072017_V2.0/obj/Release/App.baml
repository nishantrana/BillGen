   M S B A M L   `   `   `  ����   BillGen    BillGen.App   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       �� 	Resources     IconButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name J� SnapsToDevicePixels �� Padding �� CornerRadius	 J� Effect3  -System.Windows.Media.Effects.DropShadowEffect
   ShadowDepth   Opacity   
BlurRadius T� X T� Y �� Triggers P� Property P� Value �� 
TargetName N� EnterActions g� Duration	 W� To �� 
TargetName �� TargetProperty N� ExitActions��%U  &	        �� " ��.���  � ��
 ���� ��  � ��
 ���� ��  � �� �� " ��.R��� $ border��#��z��#��z�� . #��z��#��z��$��1q�$ 0q�$ 3,3,3,3��._��� $ innerBorder�� . $��1q�$ 2q�$ 3,3,3,3��._��� # z #��z��#��z����. #��z��	   
shadow��$
 2Q�$	 0.7Q�$ 3Q�z�T� translation��$ -1Q�$ -1Q� P�  �@J�IsKeyboardFocused . . ��� $ innerBorder�� � �� �	#ff06cdff��  � ��
 ��ح�P�  �@J�IsMouseOver . . ���  � ��
 ��ح�P�  � {� .  . ���  �@J�Opacity$	 0.5Q�P�  � �� .  �� .���� .��W� $ 0:0:0D�$ 0X�$ shadow��$$ (DropShadowEffect.ShadowDepth)�W� $ 0:0:0D�$ 0X�$ translation��$ (TranslateTransform.X)�W� $ 0:0:0D�$ 0X�$ translation��$ (TranslateTransform.Y)� �� .���� .��W� $ 0:0:0D�$ 1X�$ shadow��$$ (DropShadowEffect.ShadowDepth)�W� $ 0:0:0D�$ -1X�$ translation��$ (TranslateTransform.X)�W� $ 0:0:0D�$ -1X�$ translation��$ (TranslateTransform.Y)�