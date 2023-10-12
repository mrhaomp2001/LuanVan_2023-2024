@extends('layouts.master')

@section('content')
    <h1 class="text-center">
        @isset($error)
            {{ $error }}
        @endisset
    </h1>
    <h2 class="text-center">
        @isset($message)
            {{ $message }}
        @endisset
    </h2>

    <div class="text-center">
        <a href="{{ URL::previous() }}" class="btn btn-outline-info">Trở về trang trước</a>
    </div>
@stop
