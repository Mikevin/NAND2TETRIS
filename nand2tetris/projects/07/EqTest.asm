@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1

//stack 2 - stack 1
D=D-M
@EQUAL
D;JEQ
//not equal
D=-1
@END_EQUAL
0;JMP
(EQUAL)
D=0
@END_EQUAL
0;JMP
(END_EQUAL)
//store d in SP
@SP
A=M
M=D
//inc SP
@SP
M=M+1
