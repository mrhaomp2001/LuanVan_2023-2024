<x-app-layout>
    <x-slot name="header">
        <h2 class="font-semibold text-xl text-gray-800 dark:text-gray-200 leading-tight">
            {{ __('Profile') }}
        </h2>
    </x-slot>

    <br />
    <br />
    <hr />
    <div>
        <h1>Start Debug Here!</h1>
        <br />
        <h3 class="text-white">
           Câu hỏi: {{ $question->content }}
           <br />
           @foreach ($answers as $answer)
               {{ $answer->content }}
               <br />
           @endforeach
           <br />
           Cau trả lời test: {{ $answers1->content }}
           <br />
           câu hỏi test: {{ $question1 }}
        </h3>
        <br />
        <h1>End Debug Here!</h1>
    </div>
    <hr />
    <br />
    <br />


    <div class="py-12">
        <div class="max-w-7xl mx-auto sm:px-6 lg:px-8 space-y-6">
            <div class="p-4 sm:p-8 bg-white dark:bg-gray-800 shadow sm:rounded-lg">
                <div class="max-w-xl">
                    @include('profile.partials.update-profile-information-form')
                </div>
            </div>

            <div class="p-4 sm:p-8 bg-white dark:bg-gray-800 shadow sm:rounded-lg">
                <div class="max-w-xl">
                    @include('profile.partials.update-password-form')
                </div>
            </div>

            <div class="p-4 sm:p-8 bg-white dark:bg-gray-800 shadow sm:rounded-lg">
                <div class="max-w-xl">
                    @include('profile.partials.delete-user-form')
                </div>
            </div>
        </div>
    </div>
</x-app-layout>
