@extends('layouts.master')

@section('content')
    <!-- Session Status -->
    <div class="container-fluid pt-4 px-4">
        <div class="row bg-secondary rounded justify-content-center mx-0">
            <div class="row text-center">
                <div class="col-12">
                    <div class="bg-secondary rounded p-4 ">
                        <x-auth-session-status class="mb-4" :status="session('status')" />
                        <h2>Đăng nhập vào hệ thống</h2>
                        <form method="POST" action="{{ route('login') }}">
                            @csrf

                            <!-- Email Address -->
                            <div>
                                <x-input-label for="username" value="Tài khoản: " />
                                <x-text-input id="username" class="mt-1 w-full border" type="text" name="username" :value="old('username')" required autofocus autocomplete="username" />
                                <x-input-error :messages="$errors->get('username')" class="mt-2" />
                            </div>

                            <!-- Password -->
                            <div class="mt-4">
                                <x-input-label for="password" value="Mật khẩu" />

                                <x-text-input id="password" class="block mt-1 w-full" type="password" name="password" required autocomplete="current-password" />

                                <x-input-error :messages="$errors->get('password')" class="mt-2" />
                            </div>

                            <div class="flex items-center justify-end mt-4">


                                <x-primary-button class="btn btn-outline-primary">
                                    {{ __('Log in') }}
                                </x-primary-button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
@stop
