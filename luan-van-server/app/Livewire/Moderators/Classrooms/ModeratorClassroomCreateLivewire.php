<?php

namespace App\Livewire\Moderators\Classrooms;

use App\Models\Classroom;
use Illuminate\Support\Facades\Storage;
use Livewire\Component;
use Livewire\WithFileUploads;
use Livewire\Attributes\Rule;

class ModeratorClassroomCreateLivewire extends Component
{
    use WithFileUploads;

    public $name;
    public $description;
    public $theme_color = "#ffffff";
    public $image;

    public function mount()
    {
    }

    public function save()
    {
        $this->validate(
            [
                'name' => ["required", "min:3", 'max:64'],
                'description' => ["required", "min:3", 'max:512'],
            ],
            [
                'name.required' => "Cần nhập tên",
                'name.min' => "Cần nhập tên với tối thiểu :min ký tự",
                'name.max' => "Cần nhập tên với tối đa :max ký tự",

                'description.required' => "Cần nhập miêu tả",
                'description.min' => "Cần nhập miêu tả với tối thiểu :min ký tự",
                'description.max' => "Cần nhập miêu tả với tối đa :max ký tự",
                'image.image' => "Cần nhập hình ảnh",
            ]
        );

        $classroom = Classroom::create([
            'name' => $this->name,
            'description' => $this->description,
            'theme_color' => $this->theme_color,
            'user_id' => auth()->user()->id,
            'is_open' => false,
        ]);

        if (isset($this->image)) {
            Storage::disk('public')->putFileAs("classrooms/avatars", $this->image, $classroom->id . '.png');
        }

        return $this->redirect(route("moderator.classrooms.index"), navigate: true);
    }

    public function render()
    {
        return view('livewire.moderators.classrooms.moderator-classroom-create-livewire');
    }
}