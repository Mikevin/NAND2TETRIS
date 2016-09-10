//initialization
@256
D=A
@SP
M=D
@300
D=A
@LCL
M=D
@400
D=A
@ARG
M=D
@3000
D=A
@THIS
M=D
@3010
D=A
@THAT
M=D
@5
D=A
@TMP
M=D
//initialization end
//start CPush constant 10
@10
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 10
//start CPop local 0
// Local 0
@LCL
                             D=M
                             @0
                             A=D+A
                             D=M
@SP
A=M
M=D
@SP
M=M+1
//end Local 0
//end CPop local 0
//start CPush constant 21
@21
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 21
//start CPush constant 22
@22
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 22
//start CPop argument 2
// Argument 2
@ARG
                             D=M
                             @2
                             A=D+A
                             D=M
@SP
A=M
M=D
@SP
M=M+1
//end Argument 2
//end CPop argument 2
//start CPop argument 1
// Argument 1
@ARG
                             D=M
                             @1
                             A=D+A
                             D=M
@SP
A=M
M=D
@SP
M=M+1
//end Argument 1
//end CPop argument 1
//start CPush constant 36
@36
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 36
//start CPop this 6
// This 6
@THIS
                             D=M
                             @6
                             A=D+A
                             D=M
@SP
A=M
M=D
@SP
M=M+1
//end This 6
//end CPop this 6
//start CPush constant 42
@42
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 42
//start CPush constant 45
@45
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 45
//start CPop that 5
// That 5
@THAT
                             D=M
                             @5
                             A=D+A
                             D=M
@SP
A=M
M=D
@SP
M=M+1
//end That 5
//end CPop that 5
//start CPop that 2
// That 2
@THAT
                             D=M
                             @2
                             A=D+A
                             D=M
@SP
A=M
M=D
@SP
M=M+1
//end That 2
//end CPop that 2
//start CPush constant 510
@510
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 510
//start CPop temp 6
// Temp 6
@TMP
                             D=M
                             @6
                             A=D+A
                             D=M
@SP
A=M
M=D
@SP
M=M+1
//end Temp 6
//end CPop temp 6
//start CPush local 0
//Local0
@LOCAL
D=M
@0
D=D+A
@R13
M=D
@SP
M=M-1
@SP
A=M
D=M
@R13
M=D
//endLocal0
//end CPush local 0
//start CPush that 5
//That5
@THAT
D=M
@5
D=D+A
@R13
M=D
@SP
M=M-1
@SP
A=M
D=M
@R13
M=D
//endThat5
//end CPush that 5
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
//start CPush argument 1
//Argument1
@ARG
D=M
@1
D=D+A
@R13
M=D
@SP
M=M-1
@SP
A=M
D=M
@R13
M=D
//endArgument1
//end CPush argument 1
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
//start CPush this 6
//This6
@THIS
D=M
@6
D=D+A
@R13
M=D
@SP
M=M-1
@SP
A=M
D=M
@R13
M=D
//endThis6
//end CPush this 6
//start CPush this 6
//This6
@THIS
D=M
@6
D=D+A
@R13
M=D
@SP
M=M-1
@SP
A=M
D=M
@R13
M=D
//endThis6
//end CPush this 6
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
//start CPush temp 6
//Temp6
@TMP
D=M
@6
D=D+A
@R13
M=D
@SP
M=M-1
@SP
A=M
D=M
@R13
M=D
//endTemp6
//end CPush temp 6
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
