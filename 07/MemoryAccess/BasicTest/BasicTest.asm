//push constant 10
@10
D=A
@SP
A=M
M=D
@SP
M=M+1
//pop LCL 0
@0
D=A
@SP
M=M-1
A=M
D=M
@foo
M=D
@0
D=A
@LCL
A=D+M
D=A
@bar
M=D
@foo
D=M
@bar
A=M
M=D
//push constant 21
@21
D=A
@SP
A=M
M=D
@SP
M=M+1
//push constant 22
@22
D=A
@SP
A=M
M=D
@SP
M=M+1
//pop Arg 2
@2
D=A
@SP
M=M-1
A=M
D=M
@foo
M=D
@2
D=A
@ARG
A=D+M
D=A
@bar
M=D
@foo
D=M
@bar
A=M
M=D
//pop Arg 1
@1
D=A
@SP
M=M-1
A=M
D=M
@foo
M=D
@1
D=A
@ARG
A=D+M
D=A
@bar
M=D
@foo
D=M
@bar
A=M
M=D
//push constant 36
@36
D=A
@SP
A=M
M=D
@SP
M=M+1
//pop This 6
@6
D=A
@SP
M=M-1
A=M
D=M
@foo
M=D
@6
D=A
@THIS
A=D+M
D=A
@bar
M=D
@foo
D=M
@bar
A=M
M=D
//push constant 42
@42
D=A
@SP
A=M
M=D
@SP
M=M+1
//push constant 45
@45
D=A
@SP
A=M
M=D
@SP
M=M+1
//pop That 5
@5
D=A
@SP
M=M-1
A=M
D=M
@foo
M=D
@5
D=A
@THAT
A=D+M
D=A
@bar
M=D
@foo
D=M
@bar
A=M
M=D
//pop That 2
@2
D=A
@SP
M=M-1
A=M
D=M
@foo
M=D
@2
D=A
@THAT
A=D+M
D=A
@bar
M=D
@foo
D=M
@bar
A=M
M=D
//push constant 510
@510
D=A
@SP
A=M
M=D
@SP
M=M+1
//pop Temp 6
@SP
M=M-1
A=M
D=M
@11
M=D
@SP
//push LCL 0
@0
D=A
@LCL
A=M
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//push That 5
@5
D=A
@THAT
A=M
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//Add
@SP
A=M
A=A-1
D=M
@SP
M=M-1
M=M-1
A=M
M=D+M
@SP
M=M+1
//push Arg 1
@1
D=A
@ARG
A=M
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//Sub
@SP
A=M
A=A-1
D=M
D=-D
@SP
M=M-1
M=M-1
A=M
M=D+M
@SP
M=M+1
//push This 6
@6
D=A
@THIS
A=M
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//push This 6
@6
D=A
@THIS
A=M
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//Add
@SP
A=M
A=A-1
D=M
@SP
M=M-1
M=M-1
A=M
M=D+M
@SP
M=M+1
//Sub
@SP
A=M
A=A-1
D=M
D=-D
@SP
M=M-1
M=M-1
A=M
M=D+M
@SP
M=M+1
//push temp 6
@11
D=M
@SP
A=M
M=D
@SP
M=M+1
//Add
@SP
A=M
A=A-1
D=M
@SP
M=M-1
M=M-1
A=M
M=D+M
@SP
M=M+1
