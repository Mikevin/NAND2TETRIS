//initialization
@256
D=A
@SP
M=D
//initialization end
//start CPush constant 17
@17
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 17
//start CPush constant 17
@17
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 17
//eq
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1
@SP
A=M
D=D-M
@EQUAL0
D;JEQ
D=-1
@END_EQUAL0
0;JMP
(EQUAL0)
D=0
@END_EQUAL0
0;JMP
(END_EQUAL0)
@SP
A=M
M=D
@SP
M=M+1
//eq end
//start CPush constant 17
@17
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 17
//start CPush constant 16
@16
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 16
//eq
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1
@SP
A=M
D=D-M
@EQUAL1
D;JEQ
D=-1
@END_EQUAL1
0;JMP
(EQUAL1)
D=0
@END_EQUAL1
0;JMP
(END_EQUAL1)
@SP
A=M
M=D
@SP
M=M+1
//eq end
//start CPush constant 16
@16
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 16
//start CPush constant 17
@17
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 17
//eq
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1
@SP
A=M
D=D-M
@EQUAL2
D;JEQ
D=-1
@END_EQUAL2
0;JMP
(EQUAL2)
D=0
@END_EQUAL2
0;JMP
(END_EQUAL2)
@SP
A=M
M=D
@SP
M=M+1
//eq end
//start CPush constant 892
@892
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 892
//start CPush constant 891
@891
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 891
//lt
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1
@SP
A=M
D=D-M
@TRUE3
D;JLT
D=-1
@END_COMPARISON3
0;JMP
(TRUE)
D=0
@END_COMPARISON3
0;JMP
(END_COMPARISON3)
@SP
A=M
M=D
@SP
M=M+1
//lt end
//start CPush constant 891
@891
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 891
//start CPush constant 892
@892
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 892
//lt
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1
@SP
A=M
D=D-M
@TRUE4
D;JLT
D=-1
@END_COMPARISON4
0;JMP
(TRUE)
D=0
@END_COMPARISON4
0;JMP
(END_COMPARISON4)
@SP
A=M
M=D
@SP
M=M+1
//lt end
//start CPush constant 891
@891
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 891
//start CPush constant 891
@891
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 891
//lt
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1
@SP
A=M
D=D-M
@TRUE5
D;JLT
D=-1
@END_COMPARISON5
0;JMP
(TRUE)
D=0
@END_COMPARISON5
0;JMP
(END_COMPARISON5)
@SP
A=M
M=D
@SP
M=M+1
//lt end
//start CPush constant 32767
@32767
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 32767
//start CPush constant 32766
@32766
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 32766
//gt
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1
@SP
A=M
D=D-M
@TRUE6
D;JGT
D=-1
@END_COMPARISON6
0;JMP
(TRUE)
D=0
@END_COMPARISON6
0;JMP
(END_COMPARISON6)
@SP
A=M
M=D
@SP
M=M+1
//gt end
//start CPush constant 32766
@32766
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 32766
//start CPush constant 32767
@32767
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 32767
//gt
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1
@SP
A=M
D=D-M
@TRUE7
D;JGT
D=-1
@END_COMPARISON7
0;JMP
(TRUE)
D=0
@END_COMPARISON7
0;JMP
(END_COMPARISON7)
@SP
A=M
M=D
@SP
M=M+1
//gt end
//start CPush constant 32766
@32766
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 32766
//start CPush constant 32766
@32766
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 32766
//gt
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1
@SP
A=M
D=D-M
@TRUE8
D;JGT
D=-1
@END_COMPARISON8
0;JMP
(TRUE)
D=0
@END_COMPARISON8
0;JMP
(END_COMPARISON8)
@SP
A=M
M=D
@SP
M=M+1
//gt end
//start CPush constant 57
@57
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 57
//start CPush constant 31
@31
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 31
//start CPush constant 53
@53
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 53
//add
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1
@SP
A=M
D=D+M
@SP
A=M
M=D
@SP
M=M+1
//add end
//start CPush constant 112
@112
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 112
//sub
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1
@SP
A=M
D=M-D
@SP
A=M
M=D
@SP
M=M+1
//sub end
//neg
@SP
M=M-1
@SP
A=M
D=M
D=-D
@SP
A=M
M=D
@SP
M=M+1
//neg end
//and
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1
@BOOL1_FALSE9
D;JNE
@SP
M=M-1

@SP
A=M
D=M

@END9
D;JNE
(BOOL1_FALSE9)
@SP
M=M-1

D=-1
(END9)
@SP
A=M
M=D

@SP
M=M+1

@SP
A=M
M=D
@SP
M=M+1
//and end
//start CPush constant 82
@82
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 82
//or
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1
@1TRUE10
D;JEQ
@SP
M=M-1

@SP
A=M
D=M

@2TRUE10
D;JEQ
D=-1
@SP
A=M
M=D

@END10
0;JMP
(1TRUE10)
@SP
M=M-1

D=0
@SP
A=M
M=D

(2TRUE10)
D=-1
@SP
A=M
M=D

(END10)
@SP
A=M
M=D
@SP
M=M+1
//or end
//not

@TRUE11
D;JEQ
D=0
@SP
A=M
M=D

@SP
M=M+1

@END11
0;JMP
(TRUE11)
D=-1
@SP
A=M
M=D

@SP
M=M+1

(END11)
@SP
A=M
M=D
@SP
M=M+1
//not end
