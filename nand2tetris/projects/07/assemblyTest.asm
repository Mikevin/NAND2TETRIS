//initialization
@256
D=A
@SP
M=D
//initialization end

// push constant true
@SP
A=M
M=-1
@SP
M=M+1

// push constant false
@SP
A=M
M=0
@SP
M=M+1

// and
//dec SP
@SP
M=M-1
//grab value
A=M
D=M
//if true
@BOOL1_TRUE_1
D;JNE
//if true

(BOOL1_TRUE_1)
//dec SP
@SP
M=M-1
D=0
@END_1
0;JMP

(END_1)
//store D in SP
@SP
A=M
M=D
