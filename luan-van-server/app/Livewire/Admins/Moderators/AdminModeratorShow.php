<?php

namespace App\Livewire\Admins\Moderators;

use App\Models\User;
use Livewire\Component;
use Livewire\Attributes\Renderless;

class AdminModeratorShow extends Component
{
    public $searchQuery;

    public $moderators;

    public $users;

    #[Renderless]
    public function mount()
    {
        $this->moderators = User::where("role_id", 2)->get();

        $this->users = User::where('role_id', 1)
            ->where(function ($query) {
                $query->where('username', 'like', '%' . $this->searchQuery . '%')
                    ->orWhere('name', 'like', '%' . $this->searchQuery . '%');
            })
            ->get();

    }

    public function updated($property)
    {
        if ($property === 'searchQuery') {
            $this->users = User::where('role_id', 1)
                ->where(function ($query) {
                    $query->where('username', 'like', '%' . $this->searchQuery . '%')
                        ->orWhere('name', 'like', '%' . $this->searchQuery . '%');
                })
                ->get();
        }

        $this->moderators = User::where("role_id", 2)->get();
    }


    public function grantPermissions($id)
    {
        $user = User::find($id);
        $user->role_id = 2;
        $user->save();

        $this->reset();
        $this->moderators = User::where("role_id", 2)->get();
        $this->users = User::where('role_id', 1)
            ->where(function ($query) {
                $query->where('username', 'like', '%' . $this->searchQuery . '%')
                    ->orWhere('name', 'like', '%' . $this->searchQuery . '%');
            })
            ->get();
    }

    public function deletePermissions($id)
    {
        $user = User::find($id);
        $user->role_id = 1;
        $user->save();

        $this->reset();
        $this->moderators = User::where("role_id", 2)->get();
        $this->users = User::where('role_id', 1)
            ->where(function ($query) {
                $query->where('username', 'like', '%' . $this->searchQuery . '%')
                    ->orWhere('name', 'like', '%' . $this->searchQuery . '%');
            })
            ->get();
    }

    public function render()
    {
        return view('livewire.admins.moderators.admin-moderator-show');
    }
}
