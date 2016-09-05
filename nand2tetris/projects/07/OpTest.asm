//initialization
@257
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
//or
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1

D=D-1
@1TRUE2
D;JNE
@SP
A=M
D=M

D=D-1
@2TRUE2
D;JNE
D=-1
@SP
A=M
M=D

@END2
0;JMP
(1TRUE2)
D=0
@SP
A=M
M=D

@END2
0;JMP
(2TRUE2)
D=-1
@SP
A=M
M=D

(END2)
@SP
M=M+1
//or end
